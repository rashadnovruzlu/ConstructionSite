using ConstructionSite.DTO.AdminViewModels.Service;
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
    public class ServiceController : Controller
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;

        public ServiceController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
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
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Include(x => x.Image)
                .Include(x => x.SubServices)
                .Select(x => new ServiceViewModel
                {
                    Id = x.Id,
                    Name = x.FindName(_lang),
                    Tittle = x.FindTitle(_lang),
                    Image = x.Image.Path
                })
                .ToList();
            return View(result);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Service service, IFormFile FileData)
        {
            if (service == null)
            {
                return RedirectToAction("Index");
            }
            int imageresultID = 0;
            Image image = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (FileData is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new
                {
                    message = "file not found BadRequest"
                });
            }
            imageresultID = await FileData.SaveImage(_env, "service", image, _unitOfWork);
            if (imageresultID < 0)
            {
                Response.StatusCode = (int)HttpStatusCode.SeeOther;

                return Json(new
                {
                    message = "file not save"
                });
            }
            service.ImageId = imageresultID;
            var serviceResult = await _unitOfWork.ServiceRepository.AddAsync(service);
            if (serviceResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            else
            {
                FileData.Delete(_env, image, "service");
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return View();
        }

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
            if (id < 0)
            {
                return Json(new
                {
                    message = "content is empty"
                });
            }
            Service result = _unitOfWork.ServiceRepository.GetById(id);
            if (result == null)
            {
                return Json(new
                {
                    message = "this is empty"
                });
            }
            var data = new ServiceUpdateViewModel
            {
                id = result.Id,
                TittleAz = result.TittleAz,
                TittleEn = result.TittleEn,
                TittleRu = result.TittleRu,
                NameAz = result.NameAz,
                NameEn = result.NameEn,
                NameRu = result.NameRu,
                path = result.Image.Path,
                ImageId = result.ImageId
            };

            return View(data);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateViewModel model, IFormFile file)
        {
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var imageResult = _unitOfWork.imageRepository.GetById(model.ImageId);
            if (imageResult == null)
            {
                return Json(new
                {
                    message = "imageResult is empty"
                });
            }
            if (file is null)
            {
                return Json(new
                {
                    message = "file is empty"
                });
            }
            var imageUpdateResult = await file.UpdateAsyc(_env, imageResult, "service", _unitOfWork);
            if (!imageUpdateResult)
            {
                ModelState.AddModelError("", "image update error");
            }
            Service service = new Service
            {
                Id = model.id,
                NameAz = model.NameAz,
                NameEn = model.NameEn,
                NameRu = model.NameRu,
                TittleAz = model.TittleAz,
                TittleEn = model.TittleEn,
                TittleRu = model.TittleRu,
                ImageId = model.ImageId,
            };
            var result = await _unitOfWork.ServiceRepository.UpdateAsync(service);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "this is error");
            }

            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return Json(new
                {
                    message = "this is empty"
                });
            }
            var result = await _unitOfWork.ServiceRepository.DeleteAsync(service);
            if (result.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}