﻿using ConstructionSite.DTO.FrontViewModels.Message;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Controllers
{
    public class MessageController : Controller
    {
        private string                             _lang;
        private readonly IUnitOfWork               _unitOfWork;
        private readonly IHttpContextAccessor      _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public MessageController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }

        public IActionResult Index()
        {
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Add(MessageAddViewModel messageAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            if (messageAddViewModel == null)
            {
                ModelState.AddModelError("", "data is null");
            }
            messageAddViewModel.SendDate = DateTime.Now;
            messageAddViewModel.IsAnswerd = false;
            var messageAddViewModelResult = new Message
            {
                Name = messageAddViewModel.Name,
                Email = messageAddViewModel.Email,
                Subject = messageAddViewModel.Subject,
                UserMessage=messageAddViewModel.UserMessage,
                SendDate = messageAddViewModel.SendDate,
                IsAnswerd = messageAddViewModel.IsAnswerd
            };
            var messageDataResult = _unitOfWork.messageRepository.Add(messageAddViewModelResult);
            if (!messageDataResult.IsDone)
            {
                ModelState.AddModelError("", "data is not exists");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
            
        }

       
    }
}