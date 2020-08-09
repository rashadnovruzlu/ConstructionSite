using ConstructionSite.DTO.FrontViewModels.Message;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var messageAddViewModelResult = new Message
            {
                Name = messageAddViewModel.Name,
                Email = messageAddViewModel.Email,
                Subject = messageAddViewModel.Subject,
                UserMessage = messageAddViewModel.UserMessage,
                SendDate = messageAddViewModel.SendDate,
                IsAnswerd = messageAddViewModel.IsAnswerd
            };
            var messageDataResult = _unitOfWork
                .messageRepository
                .Add(messageAddViewModelResult);
            if (!messageDataResult.IsDone)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Home");
        }
    }
}