﻿using ConstructionSite.DTO.AdminViewModels.message;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ShowController : CoreController
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion Fields

        #region CTOR

        public ShowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            var messageAllResult = _unitOfWork.messageRepository.GetAll()
                .OrderByDescending(x => x.Id)
                .Select(x => new MesageViewModel
                {
                    id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    IsAnswerd = x.IsAnswerd,
                    SendDate = x.SendDate,
                    Subject = x.Subject,
                    Content = x.UserMessage
                })
                .OrderByDescending(x => x.id)
                .ToList();
            if (messageAllResult == null && messageAllResult.Count < 0)
            {
                ModelState.AddModelError("", "no message");
            }
            return View(messageAllResult);
        }

        #endregion INDEX

        #region DELETE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
                return RedirectToAction("Index");
            }
            var messageSelectedForDeleteResult = await _unitOfWork.messageRepository.GetByIdAsync(id);
            if (messageSelectedForDeleteResult == null)
            {
                ModelState.AddModelError("", "message not exists");
                return RedirectToAction("Index");
            }
            var messageAfterDeleteResult = await _unitOfWork.messageRepository.DeleteAsync(messageSelectedForDeleteResult);
            if (!messageAfterDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Bad Request");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}