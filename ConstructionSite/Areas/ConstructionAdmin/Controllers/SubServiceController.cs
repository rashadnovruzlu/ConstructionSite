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
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class SubServiceController : Controller
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }

            var SubServiceImage = _unitOfWork.SubServiceImageRepository.GetAll()
                .Include(x => x.Image)
                .Include(x => x.SubService)
                .Select(x => new SubServiceViewModel
                {
                    Id = x.Id,
                    ImagePath = x.Image.Path,
                    Name = x.SubService.FindName(_lang),
                    Content = x.SubService.FindContent(_lang)
                })
                .ToList();
            if (SubServiceImage == null && SubServiceImage.Count < 1)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this is empty");
            }

            _unitOfWork.Dispose();
            return View(SubServiceImage);
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Select(x => new ServiceSubServiceAddView
                {
                    Id = x.Id,
                    Name = x.FindName(_lang)
                }).ToList();
            if (result.Count < 1)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this is empty");
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
                ModelState.AddModelError("", "BadRequest");
               
            }
            if (file == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "file is null");
               
            }

          var  imageResultID = await file.SaveImage(_env, "subserver", imageSubService, _unitOfWork);
            if (imageResultID < 1)
            {
                ModelState.AddModelError("", "data not save");
            }
            subServiceImageResult.ImageId = imageSubService.Id;
            var SubServiceAddResult = await _unitOfWork.SubServiceRepository.AddAsync(subService);
            if (!SubServiceAddResult.IsDone)
            {
                ModelState.AddModelError("", "data not save");
                
            }
            subServiceImageResult.SubServiceId = subService.Id;
            var SubServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(subServiceImageResult);
            if (!SubServiceImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "added error");
            }
            _unitOfWork.Dispose();
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }

            if (id < 1)
            {
                ModelState.AddModelError("", "this is empty");
            }
            var subServiceImageResult = _unitOfWork.SubServiceImageRepository.GetAll()
                .Include(x => x.SubService)
                .Include(x => x.Image)
                .Select(x => new SubServiceUpdateViewModel
                {
                    Id = x.Id,
                    NameAz = x.SubService.NameAz,
                    NameEn = x.SubService.NameEn,
                    NameRu = x.SubService.NameRu,
                    ContentAz = x.SubService.ContentAz,
                    ContentEn = x.SubService.ContentEn,
                    ContentRu = x.SubService.ContentRu,
                    ServerId = x.SubService.Service.Id,
                    imageId = x.Image.Id,
                    ImagePath = x.Image.Path,

                    SubServiceId = x.SubService.Id
                }).FirstOrDefault(x => x.Id == id);

            if (subServiceImageResult == null)
            {
                ModelState.AddModelError("", "this is empty");
            }
            _unitOfWork.Dispose();
            return View(subServiceImageResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubServiceUpdateViewModel subServiceUpdateViewModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (subServiceUpdateViewModel == null)
            {
            }
            var resultSubServiceModel = new SubService
            {
                Id = subServiceUpdateViewModel.SubServiceId,
                ServiceId = subServiceUpdateViewModel.ServerId,
                NameAz = subServiceUpdateViewModel.NameAz,
                NameEn = subServiceUpdateViewModel.NameEn,
                NameRu = subServiceUpdateViewModel.NameRu,
                ContentRu = subServiceUpdateViewModel.ContentRu,
                ContentEn = subServiceUpdateViewModel.ContentEn,
                ContentAz = subServiceUpdateViewModel.ContentAz,
            };
            if (resultSubServiceModel == null)
            {
                ModelState.AddModelError("", "this error not exists");
            }

            var subServiceUpdateResult = await _unitOfWork.SubServiceRepository.UpdateAsync(resultSubServiceModel);
            if (!subServiceUpdateResult.IsDone)
            {
                ModelState.AddModelError("", "data update error");
                _unitOfWork.Rollback();
                return View(subServiceUpdateViewModel.Id);
            }

            if (file != null)
            {
                Image image = _unitOfWork.imageRepository.GetById(subServiceUpdateViewModel.imageId);
                if (image == null)
                {
                    ModelState.AddModelError("", "file not exists");
                    return View(subServiceUpdateViewModel.Id);
                }
                var imageResult = await file.UpdateAsyc(_env, image, "subserver", _unitOfWork);
                if (!imageResult)
                {
                    ModelState.AddModelError("", "file update error");
                }
            }
            SubServiceImage subServiceImage = new SubServiceImage
            {
                SubServiceId = subServiceUpdateViewModel.SubServiceId,
                ImageId = subServiceUpdateViewModel.imageId
            };
            var subServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(subServiceImage);
            if (!subServiceImageResult.IsDone)
            {
                ModelState.AddModelError("", "file not exists");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var subServiceImageResult = await _unitOfWork.SubServiceImageRepository.GetByIdAsync(id);
            if (subServiceImageResult == null)
            {
                ModelState.AddModelError("", "data is null");
            }
            var subServiceResult = await _unitOfWork.SubServiceRepository.GetByIdAsync(subServiceImageResult.SubServiceId);
            if (subServiceResult == null)
            {
                ModelState.AddModelError("", "not found id");
            }
            var imageResult = await _unitOfWork.imageRepository.GetByIdAsync(subServiceImageResult.ImageId);
            if (imageResult == null)
            {
                ModelState.AddModelError("", "not found id");
            }
            var resultUpdate = await _unitOfWork.SubServiceImageRepository.DeleteAsync(subServiceImageResult);
            if (!resultUpdate.IsDone)
            {
                ModelState.AddModelError("", "update error");
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
    }
}