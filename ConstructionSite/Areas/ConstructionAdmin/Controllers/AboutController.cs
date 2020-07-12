﻿using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class AboutController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region CTOR
        public AboutController(IUnitOfWork unitOfWork,
                               IWebHostEnvironment env,
                               IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }
        #endregion

        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not valid");
            }

            var result = _unitOfWork.AboutImageRepository.GetAll()
           .Include(x => x.About)
           .Include(x => x.Image)
           .Select(x => new AboutViewModel
           {
               Id = x.Id,
               aboutID = x.About.Id,
               Tittle = x.About.FindTitle(_lang),
               Content = x.About.FindContent(_lang),
               imageId = x.Image.Id,
               Image = x.Image.Path
           }).ToList();
            if (result.Count < 1)
            {
                ModelState.AddModelError("", "Data is null");
            }
            return View(result);
        }

        #endregion

        #region --Add--

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(About about, IFormFile FileData)
        {
            AboutImage aboutImage = new AboutImage();
            Image image = new Image();
            if (about == null)
            {
                ModelState.AddModelError("", "Data is null");
            }
            int imageresultID = 0;
            AboutImage aboutImage = new AboutImage();
            Image image = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid");
            }

            var aboutResult = await _unitOfWork.AboutRepository.AddAsync(about);
            if (!aboutResult.IsDone)
            {
                if (FileData is null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotExtended;

                    ModelState.AddModelError("", "The file is empty");
                }
                imageresultID = await FileData.SaveImage(_env, "about", image, _unitOfWork);
                if (imageresultID < 0)
                {
                    ModelState.AddModelError("", "Image is not.");
                }
                aboutImage.ImageId = imageresultID;
                aboutImage.AboutId = about.Id;

                var aboutImageResult = await _unitOfWork.AboutImageRepository.AddAsync(aboutImage);

                if (aboutImageResult.IsDone)
                {
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
                    ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);

                    _unitOfWork.Rollback();
                    ModelState.AddModelError("", "About Image is not saved.");
                }
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion --Add--

        #region --Update--

        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "this data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            var result = _unitOfWork.AboutImageRepository.GetAll()
                   .Include(x => x.About)
                      .Include(x => x.Image)
                        .Select(x => new AboutUpdateViewModel
                        {
                            Id = x.Id,
                            TittleAz = x.About.TittleAz,
                            TittleEn = x.About.TittleEn,
                            TittleRu = x.About.TittleRu,
                            ContentAz = x.About.ContentAz,
                            ContentEn = x.About.ContentEn,
                            ContentRu = x.About.ContentRu,
                            Image = x.Image.Path,
                            imageId = x.Image.Id,
                            aboutID = x.AboutId
                        })
                          .FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(AboutUpdateViewModel about, IFormFile file)
        {
            if (about == null)
            {
                ModelState.AddModelError("", "this data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            About UpdateAbout = new About
            {
                Id = about.aboutID,
                ContentAz = about.ContentAz,
                ContentEn = about.ContentEn,
                ContentRu = about.ContentRu,
                TittleAz = about.TittleAz,
                TittleEn = about.TittleEn,
                TittleRu = about.TittleRu,
            };
            var aboutResult = await _unitOfWork.AboutRepository.UpdateAsync(UpdateAbout);
            if (!aboutResult.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while editing About");
            }
            if (file!=null)
            {
                Image image = _unitOfWork.imageRepository.GetById(about.imageId);
                if (image == null)
                {

                }
                var imageUpdateAfterResult = await file.UpdateAsyc(_env, image, "about", _unitOfWork);
                if (!imageUpdateAfterResult)
                {
                    ModelState.AddModelError("", "data is null");
                }
            }
            
            
            var updateAboutImage = new AboutImage
            {
                Id = about.Id,
                ImageId = about.imageId,
                AboutId = UpdateAbout.Id,
            };
            var AboutImageResult =
             await _unitOfWork.AboutImageRepository.UpdateAsync(updateAboutImage);
            if (!AboutImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this is about update error");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion --Update--

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid");
            }
            if (id < 0)
            {
                ModelState.AddModelError("", "NULL");
            }
            var AboutImageResult = await _unitOfWork.AboutImageRepository.GetByIdAsync(id);
            if (AboutImageResult == null)
            {
                ModelState.AddModelError("", "About Image is null");
            }
            var aboutResult = await _unitOfWork.AboutRepository.GetByIdAsync(AboutImageResult.AboutId);
            if (aboutResult == null)
            {
                ModelState.AddModelError("", "About is null");
            }
            var aboutDeleteResult = await _unitOfWork.AboutRepository.DeleteAsync(aboutResult);
            if (!aboutDeleteResult.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while deleting About");
            }
           
            var image = await _unitOfWork.imageRepository.GetByIdAsync(AboutImageResult.ImageId);
            if (image is null)
            {
                ModelState.AddModelError("", "Image is null");
            }
            var imageResult = await _unitOfWork.imageRepository.DeleteAsync(image);
            if (imageResult.IsDone)
            {

                ModelState.AddModelError("", "data is null");
               
            }
           var imageResult= ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);
           
            if (!imageResult)
            {
                ModelState.AddModelError("", "an error whene delete data");
            }
           
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion
    }
}