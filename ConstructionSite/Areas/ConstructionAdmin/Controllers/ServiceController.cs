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

        #endregion Fields

        #region CTOR

        public ServiceController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.getLanguages();
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

        #endregion INDEX

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
        public async Task<IActionResult> Add(ServiceAddViewModel serviceAddViewModel, IFormFile FileData)
        {
            Image image = new Image();
            if (serviceAddViewModel == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (FileData == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "NULL");
            }
            var imageResultID = await FileData.SaveImage(_env, "service", image, _unitOfWork);
            if (!imageResultID)
            {
                Response.StatusCode = (int)HttpStatusCode.SeeOther;
                ModelState.AddModelError("", "Images are not saved");
            }

            var serviceAddViewModelResult = new Service
            {
                Id = serviceAddViewModel.ID,
                NameAz = serviceAddViewModel.NameAz,
                NameRu = serviceAddViewModel.NameRu,
                NameEn = serviceAddViewModel.NameEn,
                TittleAz = serviceAddViewModel.TittleAz,
                TittleEn = serviceAddViewModel.TittleEn,
                TittleRu = serviceAddViewModel.TittleRu,
                ImageId = image.Id
            };
            var serviceResult = await _unitOfWork.ServiceRepository.AddAsync(serviceAddViewModelResult);
            if (!serviceResult.IsDone)
            {
                _unitOfWork.Rollback();
                return RedirectToAction("Index");
            }
            else
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
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
        public async Task<IActionResult> Update(ServiceUpdateViewModel serviceUpdateViewModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (serviceUpdateViewModel == null)
            {
                ModelState.AddModelError("", "data is not exists");
                return RedirectToAction("Index");
            }
            var imageResult = _unitOfWork.imageRepository.GetById(serviceUpdateViewModel.ImageId);

            if (imageResult == null)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (file != null)
            {
                var imageUpdateResult = await file.UpdateAsyc(_env, imageResult, "service", _unitOfWork);
                if (!imageUpdateResult)
                {
                    ModelState.AddModelError("", "update is errors");
                }
            }
            else
            {
                ModelState.AddModelError("", "FILE NULL");
            }
           
            var serviceAddViewModelResult = new Service
            {
                Id = serviceUpdateViewModel.id,
                NameAz = serviceUpdateViewModel.NameAz,
                NameEn = serviceUpdateViewModel.NameEn,
                NameRu = serviceUpdateViewModel.NameRu,
                TittleAz = serviceUpdateViewModel.TittleAz,
                TittleEn = serviceUpdateViewModel.TittleEn,
                TittleRu = serviceUpdateViewModel.TittleRu,
                ImageId = serviceUpdateViewModel.ImageId,
            };
            var result = await _unitOfWork.ServiceRepository.UpdateAsync(serviceAddViewModelResult);
            if (result.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            else
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This is error");
            }

            return View(serviceUpdateViewModel);
        }

        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            Service serviceResult = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            if (serviceResult == null)
            {
                return RedirectToAction("Index");
            }
            var serviceDeleteResult = await _unitOfWork.ServiceRepository
                                                        .DeleteAsync(serviceResult);
            if (!serviceDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This portfolio was not delete");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");



            //if (id < 1)
            //{
            //    ModelState.AddModelError("", "Data is not exists");
            //}
            //var serviceIDResult = await _unitOfWork.ServiceRepository.GetByIdAsync(id);
            //if (serviceIDResult == null)
            //{
            //    ModelState.AddModelError("", "NULL");
            //}
            //var result = await _unitOfWork.ServiceRepository.DeleteAsync(serviceIDResult);
            //if (!result.IsDone)
            //{
            //    ModelState.AddModelError("", "Data cannot delete");

            //    _unitOfWork.Rollback();
            //}
            //_unitOfWork.Dispose();
            //return RedirectToAction("Index");
        }
        #endregion DELETE
    }
}