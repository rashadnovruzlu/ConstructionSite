using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Service;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Service;
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

    public class ServiceController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private readonly IServiceImageFacade _serviceImageFacade;
        private readonly IServiceFacade _serviceFacade;

        #endregion Fields

        #region CTOR

        public ServiceController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor,
                                 IServiceFacade serviceFacade,
                                 IServiceImageFacade serviceImageFacade)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _serviceFacade = serviceFacade;
            _serviceImageFacade = serviceImageFacade;
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
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Include(x => x.ServiceImages)
                .Include(x => x.SubServices)
                .Select(x => new ServiceViewModel
                {
                    Id = x.Id,
                    Name = x.FindName(_lang),
                    Tittle = x.FindTitle(_lang),
                    //Image = x.Image.Path
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
        public async Task<IActionResult> Add(ServiceAddViewModel serviceAddViewModel)
        {

            if (serviceAddViewModel == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }


            ServiceAddViewModel serviceAddViewModelForFacade = new ServiceAddViewModel
            {
                ID = serviceAddViewModel.ID,
                NameAz = serviceAddViewModel.NameAz,
                NameRu = serviceAddViewModel.NameRu,
                NameEn = serviceAddViewModel.NameEn,
                TittleAz = serviceAddViewModel.TittleAz,
                TittleEn = serviceAddViewModel.TittleEn,
                TittleRu = serviceAddViewModel.TittleRu,
            };

            var serviceResult = await _serviceFacade.Add(serviceAddViewModelForFacade);
            var resultImageID = await serviceAddViewModel.FileData.SaveImageCollectionAsync(_env, "service", _unitOfWork);
            if (serviceResult && resultImageID.Count > 0)
            {
                foreach (var item in resultImageID)
                {
                    ServiceImageAddViewModel ResultServiceImageAddViewModel = new ServiceImageAddViewModel
                    {
                        ImageId = item,
                        ServiceId = serviceAddViewModelForFacade.ID
                    };

                    await _serviceImageFacade.Add(ResultServiceImageAddViewModel);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
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
                TittleAz = result.TitleAz,
                TittleEn = result.TitleEn,
                TittleRu = result.TitleRu,
                NameAz = result.NameAz,
                NameEn = result.NameEn,
                NameRu = result.NameRu,

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
                TitleAz = serviceUpdateViewModel.TittleAz,
                TitleEn = serviceUpdateViewModel.TittleEn,
                TitleRu = serviceUpdateViewModel.TittleRu,
                //ImageId = serviceUpdateViewModel.ImageId,
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