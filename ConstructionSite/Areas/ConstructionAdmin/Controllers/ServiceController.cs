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
        #region Fields
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region CTOR
        public ServiceController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
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
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (FileData is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "NULL");
            }
            imageresultID = await FileData.SaveImage(_env, "service", image, _unitOfWork);
            if (imageresultID < 0)
            {
                Response.StatusCode = (int)HttpStatusCode.SeeOther;
                ModelState.AddModelError("", "Images are not saved");
            }
            service.ImageId = image.Id;
            var serviceResult = await _unitOfWork.ServiceRepository.AddAsync(service);
            if (serviceResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            else
            {
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return View();
        }

        #endregion

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 0)
            {
                ModelState.AddModelError("", "Content is empty");
            }
            Service result = _unitOfWork.ServiceRepository.GetById(id);
            if (result == null)
            {
                ModelState.AddModelError("", "Service is NULL");
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

        [HttpPost]
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
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (file is null)
            {
                ModelState.AddModelError("", "NULL");
            }
            var imageUpdateResult = await file.UpdateAsyc(_env, imageResult, "service", _unitOfWork);
            if (!imageUpdateResult)
            {
                ModelState.AddModelError("", "Errors occured while editing Images");
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
                ModelState.AddModelError("", "This is error");
            }

            return View();
        }

        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "Data is not exists");
            }
            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            if (service == null)
            {
                ModelState.AddModelError("", "NULL");
            }
            var result = await _unitOfWork.ServiceRepository.DeleteAsync(service);
            if (!result.IsDone)
            {
                ModelState.AddModelError("", "Data cannot delete");

                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
        #endregion
    }
}