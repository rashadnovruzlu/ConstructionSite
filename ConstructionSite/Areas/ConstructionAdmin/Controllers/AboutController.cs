using ConstructionSite.DTO.AdminViewModels.About;
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

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
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
                ModelState.AddModelError("", "NULL");
            }
            return View(result);
        }

        #endregion

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
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
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }

            var aboutResult = await _unitOfWork.AboutRepository.AddAsync(about);
            if (!aboutResult.IsDone)
            {
                ModelState.AddModelError("", "Data is not saved.");
            }
            if (FileData == null)
            {
                ModelState.AddModelError("", "File not exists");
            }
            int imageresultID = await FileData.SaveImage(_env, "about", image, _unitOfWork);
            if (imageresultID < 1)
            {
                ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "File is not saved.");
            }
            if (aboutResult.IsDone)
            {
                aboutImage.ImageId = image.Id;
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
                    ModelState.AddModelError("", "File is not saved");
                }
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region UPDATE
        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
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
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
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
                ModelState.AddModelError("", "Errors occured while updating About");
            }
            if (file != null)
            {
                Image image = _unitOfWork.imageRepository.GetById(about.imageId);
                if (image == null)
                {

                }
                var imageUpdateAfterResult = await file.UpdateAsyc(_env, image, "about", _unitOfWork);
                if (!imageUpdateAfterResult)
                {
                    ModelState.AddModelError("", "NULL");
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
                ModelState.AddModelError("", "Errors occured while updating About");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 0)
            {
                ModelState.AddModelError("", "NULL");
            }
            var AboutImageResult = await _unitOfWork.AboutImageRepository.GetByIdAsync(id);
            if (AboutImageResult == null)
            {
                ModelState.AddModelError("", "About Image is empty.");
            }
            var aboutResult = await _unitOfWork.AboutRepository.GetByIdAsync(AboutImageResult.AboutId);
            if (aboutResult == null)
            {
                ModelState.AddModelError("", "About is empty.");
            }
            var aboutDeleteResult = await _unitOfWork.AboutRepository.DeleteAsync(aboutResult);
            if (!aboutDeleteResult.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while deleting About");
            }

            var image = await _unitOfWork.imageRepository.GetByIdAsync(AboutImageResult.ImageId);

            if (image == null)
            {
                ModelState.AddModelError("", "NULL");
            }
            var imageResult = ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);

            if (!imageResult)
            {
                ModelState.AddModelError("", "An Error occured while deleting Image");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
        #endregion
    }
}