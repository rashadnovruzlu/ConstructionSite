using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var messageAllResult=  _unitOfWork.messageRepository.GetAll()
                .OrderByDescending(x=>x.Id)
                .ToList();
            if (messageAllResult==null&&messageAllResult.Count<0)
            {
                ModelState.AddModelError("", "no message");
            }
            return View(messageAllResult);
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
