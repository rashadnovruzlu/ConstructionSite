using ConstructionSite.DTO.FrontViewModels.Maps;
using ConstructionSite.DTO.FrontViewModels.Message;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly SharedLocalizationService _localizationHandle;

        public MessageController(IUnitOfWork unitOfWork,

                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
        }

        public IActionResult Index()
        {
            var _googelMapGeolocation = _unitOfWork
                   .ContactRepository
                   .GetAll()

                   .Select(x => new Geolocation
                   {
                       Latitude = x.lat,
                       Longitude = x.lng
                   }).FirstOrDefault();
            ViewBag.la = _googelMapGeolocation.Latitude;
            ViewBag.lo = _googelMapGeolocation.Longitude;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MessageAddViewModel messageAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            if (messageAddViewModel == null)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
            }
            messageAddViewModel.SendDate = DateTime.Now;
            messageAddViewModel.IsAnswerd = false;
             var messageAddViewModelResult = messageAddViewModel.Mapped<Message>();
            var messageDataResult = _unitOfWork
                .messageRepository
                .Add(messageAddViewModelResult);
            if (!messageDataResult.IsDone)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
                return RedirectToAction("Index","Home");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Home");
        }
    }
}