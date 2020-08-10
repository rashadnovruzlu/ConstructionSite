using ConstructionSite.DTO.AdminViewModels.message;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class ShowController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region CTOR
        public ShowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region INDEX
        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
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
                    Content=x.UserMessage
                })
                .ToList();
            if (messageAllResult == null && messageAllResult.Count < 0)
            {
                ModelState.AddModelError("", "no message");
            }
            return View(messageAllResult);
        }
        #endregion

        #region DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
