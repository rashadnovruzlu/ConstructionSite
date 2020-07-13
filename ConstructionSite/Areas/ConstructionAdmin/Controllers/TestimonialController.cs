using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class TestimonialController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region CTOR
        public TestimonialController(IUnitOfWork unitOfWork,
                                     IWebHostEnvironment env,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            return View();
        }

        #endregion

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CustomerFeedback customerFeedback)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (customerFeedback == null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerFeedbackResult = await _unitOfWork.customerFeedbackRepository.AddAsync(customerFeedback);

            if (!customerFeedbackResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This data is not added");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region UPDATE

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            var customerFeedbackUpdateResult = await _unitOfWork.customerFeedbackRepository.GetByIdAsync(id);
            if (customerFeedbackUpdateResult == null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerFeedbackUpdate = new CustomerUpdateModel
            {
                Id = customerFeedbackUpdateResult.Id,
                ContentAz = customerFeedbackUpdateResult.ContentAz,
                ContentRu = customerFeedbackUpdateResult.ContentRu,
                ContentEn = customerFeedbackUpdateResult.ContentEn,
                FullName = customerFeedbackUpdateResult.FullName,
                Position = customerFeedbackUpdateResult.Position
            };
            if (customerFeedbackUpdate == null)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This data is null or empty");
            }
            _unitOfWork.Dispose();
            return View(customerFeedbackUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CustomerFeedback customerFeedback)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (customerFeedback == null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerFeedbackUpdateResult = await _unitOfWork.customerFeedbackRepository.UpdateAsync(customerFeedback);
            if (!customerFeedbackUpdateResult.IsDone)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "Id is null");
            }
            var customerFeedbackResult = await _unitOfWork.customerFeedbackRepository.GetByIdAsync(id);
            if (customerFeedbackResult != null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerFeedbackDeleteResult = await _unitOfWork.customerFeedbackRepository.DeleteAsync(customerFeedbackResult);
            if (!customerFeedbackDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This data is null or empty");
            }
            _unitOfWork.Dispose();
            return View();
        }

        #endregion
    }
}