using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.About;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class AboutController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutFacade _aboutFacade;
        private readonly IAboutImageFacade _aboutImageFacade;

        #endregion Fields

        #region CTOR

        public AboutController(IUnitOfWork unitOfWork,
                               IWebHostEnvironment env,
                               IHttpContextAccessor httpContextAccessor,
                               IAboutFacade aboutFacade,
                               IAboutImageFacade aboutImageFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _aboutFacade = aboutFacade;
            _aboutImageFacade = aboutImageFacade;
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }

            var result = _aboutFacade.
                GetAll(_lang).
                   ToList(); ;
            if (result.Count < 1)
            {
                ModelState.AddModelError("", "NULL");
            }
            return View(result);
        }

        #endregion INDEX

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AboutAddViewModel aboutAddViewModel)
        {
            if (aboutAddViewModel == null)
            {
                ModelState.AddModelError("", "Data is null");
            }
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var resultAbout = await _aboutFacade.AddAsync(aboutAddViewModel);
            var resultImage = await aboutAddViewModel.FileData.SaveImageCollectionAsync(_env, "About", _unitOfWork);
            return await SaveAll(resultAbout, resultImage);
        }



        #endregion CREATE

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
                            aboutId = x.AboutId
                        })
                          .FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(AboutUpdateViewModel aboutUpdateViewModel)
        {
            if (aboutUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            var resultaboutUpdateViewModel = await _aboutFacade.Update(aboutUpdateViewModel);
            if (aboutUpdateViewModel.files != null)
            {
                var result = _unitOfWork.AboutImageRepository.GetAll().Where(x => x.AboutId == resultaboutUpdateViewModel.Data.Id)
              .Take(aboutUpdateViewModel.files.Count)
              .Select(x => x.Image).ToArray();
                await SaveAll(aboutUpdateViewModel, resultaboutUpdateViewModel, result);
            }


            var issuccess = await _unitOfWork.CommitAsync();
            if (issuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        private async Task SaveAll(AboutUpdateViewModel aboutUpdateViewModel, RESULT<About> resultaboutUpdateViewModel, Image[] result)
        {
            if (resultaboutUpdateViewModel.IsDone && aboutUpdateViewModel.files.Count > 0)
            {
                _env.Delete(result, "about", _unitOfWork);
                var resultListImageId = await aboutUpdateViewModel.files.SaveImageCollectionAsync(_env, "about", _unitOfWork);
                foreach (var ImageID in resultListImageId)
                {
                    AboutImage resultAboutImage = new AboutImage
                    {
                        ImageId = ImageID,
                        AboutId = resultaboutUpdateViewModel.Data.Id

                    };
                    await _unitOfWork.AboutImageRepository.UpdateAsync(resultAboutImage);
                }


            }
        }

        #endregion UPDATE

        #region DELETE

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "NULL");
            }
            var AboutImageResult = _unitOfWork.AboutImageRepository.GetById(id);

            var aboutResult = _unitOfWork.AboutRepository.GetById(AboutImageResult.AboutId);

            var imageResult = _unitOfWork.imageRepository.GetById(AboutImageResult.ImageId);

            if (aboutResult != null && imageResult != null)
            {
                var aboutDeleteResult = _unitOfWork.AboutRepository.Delete(aboutResult);
                var imageDeleteResult = ImageExtensions.DeleteAsyc(_env, imageResult, "about", _unitOfWork);
                if (aboutDeleteResult.IsDone && imageDeleteResult)
                {
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "delete has been error");
                    RedirectToAction("Index");
                }
            }

            _unitOfWork.Rollback();
            return RedirectToAction("Index");
        }

        #endregion DELETE

        #region ::private::
        private async Task<IActionResult> SaveAll(RESULT<Entity.Models.About> resultAbout, List<int> resultImage)
        {
            if (resultAbout.IsDone && resultImage.Count > 0)
            {
                foreach (var item in resultImage)
                {
                    var resultAboutImageAddViewModel = new AboutImageAddViewModel
                    {
                        ImageId = item,
                        AboutId = resultAbout.Data.Id
                    };
                    await _aboutImageFacade.AddAsync(resultAboutImageAddViewModel);
                }
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                }
            }
            return View();
        }
        #endregion
    }
}