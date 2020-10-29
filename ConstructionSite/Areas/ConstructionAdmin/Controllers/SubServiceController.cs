﻿using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Services;
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
            var result = _subServiceFacade.GetAll(_lang);
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
                return RedirectToAction("Index");
            }
            if (subServiceAddModel == null)
            {
                ModelState.AddModelError("", "News is empty");
                return RedirectToAction("Index");
            }

            try
            {
                var resulBlogAddViewModel = await _subServiceFacade.Add(subServiceAddModel);
                var resultImage = await subServiceAddModel.file.SaveImageCollectionAsync(_env, "service", _unitOfWork);
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
                    else
                    {
                        _unitOfWork.Rollback();
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
            ViewBag.services = _subServiceFacade.GetServices(_lang);
            var resultUpdate = _subServiceFacade.GetForUpdate(id);
            return View(resultUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SubServiceUpdateViewModel subServiceUpdateViewModel)
        {
            if (subServiceUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data not exists");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            try
            {

                if (subServiceUpdateViewModel.files != null && subServiceUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < subServiceUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == subServiceUpdateViewModel.ImageID[i]);
                            await subServiceUpdateViewModel.files[i].UpdateAsyc(_env, image, "about", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (subServiceUpdateViewModel.files != null)
                {
                    try
                    {
                        var emptyImage = _unitOfWork.SubServiceRepository.Find(x => x.Id == subServiceUpdateViewModel.Id);

                        var imagesid = await subServiceUpdateViewModel.files.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
                        foreach (var item in imagesid)
                        {
                            var resultData = new SubServiceImage
                            {
                                SubServiceId = emptyImage.Id,
                                ImageId = item
                            };
                            await _unitOfWork.SubServiceImageRepository.AddAsync(resultData);
                        }
                        await _unitOfWork.CommitAsync();
                    }
                    catch
                    {
                    }
                }
                var resultAbout = await _subServiceFacade.Update(subServiceUpdateViewModel);
                if (resultAbout.IsDone)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
            }
            return View(subServiceUpdateViewModel);
        }

        #endregion UPDATE

        #region DELETE

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var subServiceImageResult = await _unitOfWork.SubServiceImageRepository.GetByIdAsync(id);
        //    if (subServiceImageResult == null)
        //    {
        //        ModelState.AddModelError("", "Data is null");
        //    }
        //    var subServiceResult = await _unitOfWork.SubServiceRepository.GetByIdAsync(subServiceImageResult.SubServiceId);
        //    if (subServiceResult == null)
        //    {
        //        ModelState.AddModelError("", "Sub Service Not Found");
        //    }
        //    var imageResult = await _unitOfWork.imageRepository.GetByIdAsync(subServiceImageResult.ImageId);
        //    if (imageResult == null)
        //    {
        //        ModelState.AddModelError("", "Image Not Found");
        //    }
        //    var resultUpdate = await _unitOfWork.SubServiceImageRepository.DeleteAsync(subServiceImageResult);
        //    if (!resultUpdate.IsDone)
        //    {
        //        ModelState.AddModelError("", "Errors occured while deleting SubService Images");
        //        _unitOfWork.Rollback();
        //    }
        //    _unitOfWork.Dispose();
        //    return RedirectToAction("Index");
        //}

        #endregion DELETE
    }
}