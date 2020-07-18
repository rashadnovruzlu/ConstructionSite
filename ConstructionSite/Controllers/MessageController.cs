using ConstructionSite.DTO.FrontViewModels.Message;
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
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
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
            return RedirectToAction("Index", "Message", new { area = "area" });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MessageAddViewModel messageAddViewModel)
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
                SendDate = messageAddViewModel.SendDate,
                IsAnswerd = messageAddViewModel.IsAnswerd
            };
            var messageDataResult = await _unitOfWork.messageRepository.AddAsync(messageAddViewModelResult);
            if (!messageDataResult.IsDone)
            {
                ModelState.AddModelError("", "data is");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

       
    }
}