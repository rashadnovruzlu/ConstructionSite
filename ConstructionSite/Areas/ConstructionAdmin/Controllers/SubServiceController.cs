using ConstructionSite.DTO.AdminViewModels.SubService;
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
   
    public class SubServiceController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion Fields

        #region CTOR

        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
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
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Select(x => new ServiceSubServiceAddView
                {
                    Id = x.Id,
                    Name = x.FindName(_lang)
                }).ToList();
            if (result.Count < 1)
            {
                _unitOfWork.Dispose();
                ModelState.AddModelError("", "This is empty");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            ViewBag.data = result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SubService subService, IFormFile file)
        {
            SubServiceImage subServiceImageResult = new SubServiceImage();
            Image imageSubService = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }
            if (file == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "File is null");
            }

            var imageResultID = await file.SaveImageAsync(_env, "subserver", imageSubService, _unitOfWork);
            if (!imageResultID)
            {
                ModelState.AddModelError("", "Data didn't save");
            }

            var SubServiceAddResult = await _unitOfWork.SubServiceRepository.AddAsync(subService);
            if (!SubServiceAddResult.IsDone)
            {
                ModelState.AddModelError("", "Data didn't save");
            }
            subServiceImageResult.SubServiceId = subService.Id;
            subServiceImageResult.ImageId = imageSubService.Id;
            var SubServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(subServiceImageResult);
            if (!SubServiceImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while adding SubService");
            }
            _unitOfWork.Dispose();

            return RedirectToAction("Index");
        }

        #endregion CREATE

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "This is empty");
                return RedirectToAction("Index");
            }
            var subserviceUpdateImageResult = _unitOfWork.SubServiceImageRepository
                     .GetAll()
                     .Include(x => x.Image)
                     .Include(x => x.SubService)
                     .Select(x => new SubServiceUpdateViewModel
                     {
                         Id = x.SubServiceId,
                         imageId = x.ImageId,
                         ImagePath = x.Image.Path,
                         ContentAz = x.SubService.ContentAz,
                         ContentEn = x.SubService.ContentEn,
                         ContentRu = x.SubService.ContentRu,
                         NameAz = x.SubService.NameAz,
                         NameEn = x.SubService.NameEn,
                         NameRu = x.SubService.NameRu,
                         ServerId = x.SubService.ServiceId
                     })
                     .FirstOrDefault(x => x.Id == id);
            if (subserviceUpdateImageResult == null)
            {
                ModelState.AddModelError("", "This is empty");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return View(subserviceUpdateImageResult);
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
            //SubServiceImage subServiceImage = new SubServiceImage
            //{
            //    Id= subServiceUpdateViewModel.Id,
            //    SubServiceId = subServiceUpdateViewModel.SubServiceId,
            //    ImageId = subServiceUpdateViewModel.imageId
            //};
            //var subServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(subServiceImage);
            //if (!subServiceImageResult.IsDone)
            //{
            //    ModelState.AddModelError("", "File is not exists");
            //}
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