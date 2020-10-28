using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class SubServiceController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubServiceFacade _subServiceFacade;

        #endregion Fields

        #region CTOR

        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env,
                                    IHttpContextAccessor httpContextAccessor,
                                    ISubServiceFacade subServiceFacade)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _subServiceFacade = subServiceFacade;
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }

            var SubServiceImage = _unitOfWork.SubServiceImageRepository.GetAll()
                .Include(x => x.Image)
                .Include(x => x.SubService)
                .Select(x => new SubServiceViewModel
                {
                    Id = x.SubServiceId,
                    ImagePath = x.Image.Path,
                    Name = x.SubService.FindName(_lang),
                    Content = x.SubService.FindContent(_lang)
                })
                .ToList();
            if (SubServiceImage == null && SubServiceImage.Count < 1)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This is empty");
            }

            _unitOfWork.Dispose();
            return View(SubServiceImage);
        }

        #endregion INDEX

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }
            ViewBag.services = _subServiceFacade.GetServices(_lang);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SubServiceAddModel subServiceAddModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (subServiceAddModel == null)
            {
                ModelState.AddModelError("", "News is empty");
            }

            try
            {
                var resulBlogAddViewModel = await _subServiceFacade.Add(subServiceAddModel);
                var resultImage = await subServiceAddModel.file.SaveImageCollectionAsync(_env, "news", _unitOfWork);
                if (resulBlogAddViewModel.IsDone && resultImage.Count > 0)
                {
                    foreach (var item in resultImage)
                    {
                        var result = new SubServiceImage
                        {
                            ImageId = item,
                            SubServiceId = resulBlogAddViewModel.Data.Id
                        };
                        await _unitOfWork.SubServiceImageRepository.AddAsync(result);
                    }
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {


            }
            return View();


        }

        #endregion CREATE

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubServiceUpdateViewModel subServiceUpdateViewModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (subServiceUpdateViewModel == null)
            {
            }
            var subServiceViewUpdateModel = new SubService
            {
                Id = subServiceUpdateViewModel.Id,
                ServiceId = subServiceUpdateViewModel.ServerId,

                NameAz = subServiceUpdateViewModel.NameAz,
                NameEn = subServiceUpdateViewModel.NameEn,
                NameRu = subServiceUpdateViewModel.NameRu,
                ContentRu = subServiceUpdateViewModel.ContentRu,
                ContentEn = subServiceUpdateViewModel.ContentEn,
                ContentAz = subServiceUpdateViewModel.ContentAz,
            };
            if (subServiceViewUpdateModel == null)
            {
                ModelState.AddModelError("", "Sub Service is null");
            }

            var subServiceUpdateResult = _unitOfWork.SubServiceRepository.Update(subServiceViewUpdateModel);
            if (!subServiceUpdateResult.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while editing Sub Service");
                _unitOfWork.Rollback();
                return View(subServiceUpdateViewModel.Id);
            }
            if (file != null)
            {
                Image image = _unitOfWork.imageRepository.GetById(subServiceUpdateViewModel.imageId);
                if (image == null)
                {
                    ModelState.AddModelError("", "File is not exists");
                    return View(subServiceUpdateViewModel.Id);
                }
                var imageResult = await file.UpdateAsyc(_env, image, "subserver", _unitOfWork);
                if (!imageResult)
                {
                    ModelState.AddModelError("", "Errors occured while editing Sub Service Images");
                }
            }
            _unitOfWork.Commit();

            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion UPDATE

        #region DELETE

        public async Task<IActionResult> Delete(int id)
        {
            var subServiceImageResult = await _unitOfWork.SubServiceImageRepository.GetByIdAsync(id);
            if (subServiceImageResult == null)
            {
                ModelState.AddModelError("", "Data is null");
            }
            var subServiceResult = await _unitOfWork.SubServiceRepository.GetByIdAsync(subServiceImageResult.SubServiceId);
            if (subServiceResult == null)
            {
                ModelState.AddModelError("", "Sub Service Not Found");
            }
            var imageResult = await _unitOfWork.imageRepository.GetByIdAsync(subServiceImageResult.ImageId);
            if (imageResult == null)
            {
                ModelState.AddModelError("", "Image Not Found");
            }
            var resultUpdate = await _unitOfWork.SubServiceImageRepository.DeleteAsync(subServiceImageResult);
            if (!resultUpdate.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while deleting SubService Images");
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}