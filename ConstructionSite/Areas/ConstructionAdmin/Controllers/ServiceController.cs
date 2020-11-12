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
            var result = _serviceFacade.GetAll(_lang)
                .OrderByDescending(x => x.Id)
                .ToList();

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
                return RedirectToAction("Index");
            }

            try
            {
                var serviceResult = await _serviceFacade.Add(serviceAddViewModel);
                var resultImageIDs = await serviceAddViewModel.FileData.SaveImageCollectionAsync(_env, "service", _unitOfWork);
                if (serviceResult.IsDone && resultImageIDs.Count > 0)
                {
                    await SaveServiceAndImages(serviceResult.Data.Id, resultImageIDs);
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        return View();
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
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index");
            }

            var result = _serviceFacade.GetForUpdate(id);
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ServiceUpdateViewModel serviceUpdateViewModel)
        {
            if (serviceUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index");
            }
            try
            {

                if (serviceUpdateViewModel.files != null && serviceUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < serviceUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == serviceUpdateViewModel.ImageID[i]);
                            await serviceUpdateViewModel.files[i].UpdateAsyc(_env, image, "service", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (serviceUpdateViewModel.files != null)
                {
                    try
                    {
                        var emptyImage = _unitOfWork.ServiceRepository.Find(x => x.Id == serviceUpdateViewModel.id);

                        var imagesid = await serviceUpdateViewModel.files.SaveImageCollectionAsync(_env, "", _unitOfWork);
                        foreach (var item in imagesid)
                        {
                            var resultData = new ServiceImage
                            {
                                ServiceId = emptyImage.Id,
                                ImageId = item
                            };
                            await _unitOfWork.ServiceImageRepstory.AddAsync(resultData);
                        }
                        await _unitOfWork.CommitAsync();
                    }
                    catch
                    {
                    }
                }
                var resultAbout = await _serviceFacade.Update(serviceUpdateViewModel);
                if (resultAbout.IsDone)
                {
                    await _unitOfWork.CommitAsync();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
            }
            return View(serviceUpdateViewModel);
        }

        #endregion UPDATE

        #region DELETE

        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Index");
            }
            if (_serviceFacade.Delete(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "errors is accured when delete File");
                return RedirectToAction("Index");
            }
        }

        #endregion DELETE

        #region ::private ::

        private async Task SaveServiceAndImages(int id, List<int> resultImageID)
        {
            try
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
            catch
            {


            }

        }

        #endregion ::private ::
    }
}