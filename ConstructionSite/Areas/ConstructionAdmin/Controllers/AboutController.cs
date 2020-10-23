using ConstructionSite.DTO.AdminViewModels.About;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        #endregion Fields

        #region CTOR

        public AboutController(IUnitOfWork unitOfWork,
                               IWebHostEnvironment env,
                               IHttpContextAccessor httpContextAccessor,
                               IAboutFacade aboutFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _aboutFacade = aboutFacade;
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
        public async Task<IActionResult> Add(AboutAddViewModel aboutAddViewModel, IFormFile FileData)
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
            var resultAfterInsert = await _aboutFacade.Insert(aboutAddViewModel, FileData);
            if (!resultAfterInsert)
            {
                ModelState.AddModelError("", "errors");
            }
            //var aboutAddViewModelResult = new About
            //{
            //    Id = aboutAddViewModel.Id,
            //    TittleAz = aboutAddViewModel.TittleAz,
            //    TittleRu = aboutAddViewModel.TittleRu,
            //    TittleEn = aboutAddViewModel.TittleEn,
            //    ContentAz = aboutAddViewModel.ContentAz,
            //    ContentEn = aboutAddViewModel.ContentEn,
            //    ContentRu = aboutAddViewModel.ContentRu
            //};
            //var aboutSaveResult = await _unitOfWork.AboutRepository.AddAsync(aboutAddViewModelResult);

            //if (!aboutSaveResult.IsDone)
            //{
            //    ModelState.AddModelError("", "Data is not saved.");
            //}
            //if (FileData == null)
            //{
            //    ModelState.AddModelError("", "File not exists");
            //    return RedirectToAction("Index");
            //}
            //bool imageSaveResult = await FileData.SaveImageAsync(_env, "about", image, _unitOfWork);
            //if (!imageSaveResult)
            //{
            //    //ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);

            //    ModelState.AddModelError("", "File is not saved.");
            //}

            //if (aboutSaveResult.IsDone && imageSaveResult)
            //{
            //    aboutImage.ImageId = image.Id;
            //    aboutImage.AboutId = aboutAddViewModelResult.Id;

            //    var aboutImageResult = await _unitOfWork.AboutImageRepository.AddAsync(aboutImage);

            //    if (aboutImageResult.IsDone)
            //    {
            //        _unitOfWork.Dispose();
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ImageExtensions.DeleteAsyc(_env, image, "about", _unitOfWork);

            //        _unitOfWork.Rollback();
            //        ModelState.AddModelError("", "File is not saved");
            //    }
            //}
            //_unitOfWork.Dispose();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Update(AboutUpdateViewModel aboutUpdateViewModel, IFormFile file)
        {
            if (aboutUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            //About UpdateAbout = new About
            //{
            //    Id = aboutUpdateViewModel.aboutID,
            //    ContentAz = aboutUpdateViewModel.ContentAz,
            //    ContentEn = aboutUpdateViewModel.ContentEn,
            //    ContentRu = aboutUpdateViewModel.ContentRu,
            //    TittleAz = aboutUpdateViewModel.TittleAz,
            //    TittleEn = aboutUpdateViewModel.TittleEn,
            //    TittleRu = aboutUpdateViewModel.TittleRu,
            //};
            //var aboutResult = await _unitOfWork.AboutRepository.UpdateAsync(UpdateAbout);
            //if (!aboutResult.IsDone)
            //{
            //    ModelState.AddModelError("", "Errors occured while updating About");
            //}
            //if (file != null)
            //{
            //    Image image = _unitOfWork.imageRepository.GetById(aboutUpdateViewModel.imageId);
            //    if (image == null)
            //    {
            //        ModelState.AddModelError("", "NULL");
            //    }
            //    var imageUpdateAfterResult = await file.UpdateAsyc(_env, image, "about", _unitOfWork);
            //    if (!imageUpdateAfterResult)
            //    {
            //        ModelState.AddModelError("", "NULL");
            //    }
            //}

            //var updateAboutImage = new AboutImage
            //{
            //    Id = aboutUpdateViewModel.Id,
            //    ImageId = aboutUpdateViewModel.imageId,
            //    AboutId = UpdateAbout.Id,
            //};
            //var AboutImageResult =
            // await _unitOfWork.AboutImageRepository.UpdateAsync(updateAboutImage);
            //if (!AboutImageResult.IsDone)
            //{
            //    _unitOfWork.Rollback();
            //    ModelState.AddModelError("", "Errors occured while updating About");
            //}
            //_unitOfWork.Dispose();
            var result = await _aboutFacade.Update(aboutUpdateViewModel, file);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
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
    }
}