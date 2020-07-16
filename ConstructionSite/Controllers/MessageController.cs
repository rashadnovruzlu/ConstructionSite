using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class MessageController : Controller
    {
        string _lang;
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
        public async Task<IActionResult> Add(Message message)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            if (message==null)
            {
                ModelState.AddModelError("", "data is null");
            }

     var messageDataResult= await  _unitOfWork.messageRepository.AddAsync(message);
            if (messageDataResult.IsDone)
            {
                ModelState.AddModelError("","data is");
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
          var messageGetByIDResult= await _unitOfWork.messageRepository.GetByIdAsync(id);
            if (messageGetByIDResult==null)
            {

            }
        var messageResult=  await  _unitOfWork.messageRepository.DeleteAsync(messageGetByIDResult);
            if (!messageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Bad Request");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
            
        }
    }
}
