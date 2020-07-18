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
            return View();
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
            return RedirectToAction("");
        }

        public async Task<IActionResult> Delte(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var messageSelectedForDeleteResult = await _unitOfWork.messageRepository.GetByIdAsync(id);
            if (messageSelectedForDeleteResult == null)
            {
                ModelState.AddModelError("", "message not exists");
            }
            var messageAfterDeleteResult = await _unitOfWork.messageRepository.DeleteAsync(messageSelectedForDeleteResult);
            if (!messageAfterDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Bad Request");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
    }
}