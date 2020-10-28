using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Service;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Service;
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
            _serviceFacade = serviceFacade;
            _serviceImageFacade = serviceImageFacade;
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
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

            var serviceResult = await _serviceFacade.Add(serviceAddViewModel);
            var resultImageID = await serviceAddViewModel.FileData.SaveImageCollectionAsync(_env, "service", _unitOfWork);
            if (serviceResult.IsDone && resultImageID.Count > 0)
            {
                await SaveServiceAndImages(serviceResult.Data.Id, resultImageID);
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                    return View();
                }
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
            var result = _unitOfWork.ServiceImageRepstory.GetAll()
              .Include(x => x.Image)
              .Include(x => x.Service)
              .Select(x => new ServiceUpdateViewModel
              {
                  id = x.Service.Id,
                  ContentAz = x.Service.ContentAz,
                  ContentEn = x.Service.ContentEn,
                  ContentRu = x.Service.ContentRu,
                  NameAz = x.Service.NameAz,
                  NameEn = x.Service.NameEn,
                  NameRu = x.Service.NameRu,
                  TittleAz = x.Service.TitleAz,
                  TittleEn = x.Service.TitleEn,
                  TittleRu = x.Service.TitleRu,
                  path = x.Image.Path
              })
              .FirstOrDefault(x => x.id == id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateViewModel serviceUpdateViewModel)
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
            var resultUpdateService = await _serviceFacade.Update(serviceUpdateViewModel);

            if (serviceUpdateViewModel.file != null)
            {
                var resultImage = _unitOfWork.ServiceImageRepstory.GetAll()
                 .Where(x => x.ServiceId == serviceUpdateViewModel.id)
                 .Select(x => x.Image)
                 .Take(serviceUpdateViewModel.file.Count)
                 .ToArray();

                _env.Delete(resultImage, "service", _unitOfWork);
                var resultServiceImageId = await serviceUpdateViewModel.file.SaveImageCollectionAsync(_env, "service", _unitOfWork);
                foreach (var item in resultServiceImageId)
                {
                    ServiceImage serviceImage = new ServiceImage
                    {
                        ImageId = item,
                        ServiceId = resultUpdateService.Data.Id
                    };
                    await _unitOfWork.ServiceImageRepstory.UpdateAsync(serviceImage);
                }
            }
            if (await _unitOfWork.CommitAsync())
            {
                return RedirectToAction("Index");
            }
            return View(serviceUpdateViewModel.id);
        }

        #endregion UPDATE

        #region DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                ModelState.AddModelError("", "data not exists");
            }
            var result = await _serviceImageFacade.Delete(id);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "can be deleted");
                return RedirectToAction("Index");
            }
        }

        #endregion DELETE

        #region ::private ::

        private async Task SaveServiceAndImages(int id, List<int> resultImageID)
        {
            foreach (var item in resultImageID)
            {
                ServiceImageAddViewModel ResultServiceImageAddViewModel = new ServiceImageAddViewModel
                {
                    ImageId = item,
                    ServiceId = id
                };

                await _serviceImageFacade.Add(ResultServiceImageAddViewModel);
            }
        }

        #endregion ::private ::
    }
}