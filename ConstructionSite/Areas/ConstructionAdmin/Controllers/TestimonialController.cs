using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        #endregion Fields

        #region CTOR

        public TestimonialController(IUnitOfWork unitOfWork,
                                     IWebHostEnvironment env,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.getLanguages();
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            var CustomerFeedbackViewModelResult = _unitOfWork.customerFeedbackRepository.GetAll()
                    .Select(x => new CustomerViewModel
                    {
                        id = x.Id,
                        Content = x.FindContent(_lang),
                        FullName = x.FullName,
                        Position = x.Position
                    })
                    .ToList();
            return View(CustomerFeedbackViewModelResult);
        }

        #endregion INDEX

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
        public async Task<IActionResult> Add(CustomerAddViewModel customerAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (customerAddViewModel == null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerAddViewModelResult = new CustomerFeedback
            {
                ContentAz = customerAddViewModel.ContentAz,
                ContentEn = customerAddViewModel.ContentEn,
                ContentRu = customerAddViewModel.ContentRu,
                FullName = customerAddViewModel.FullName,
                Position = customerAddViewModel.Position
            };
            var customerFeedbackAddedResult = await _unitOfWork.customerFeedbackRepository.AddAsync(customerAddViewModelResult);

            if (! customerFeedbackAddedResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This data is not added");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion CREATE

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
            var customerFeedbackUpdate = new CustomerViewUpdateModel
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
        public async Task<IActionResult> Update(CustomerViewUpdateModel customerViewUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (customerViewUpdateModel == null)
            {
                ModelState.AddModelError("", "This data is null or empty");
            }
            var customerViewUpdateModelResult = new CustomerFeedback
            {
                Id = customerViewUpdateModel.Id,
                ContentAz = customerViewUpdateModel.ContentAz,
                ContentEn = customerViewUpdateModel.ContentEn,
                ContentRu = customerViewUpdateModel.ContentRu,
                FullName = customerViewUpdateModel.FullName,
                Position = customerViewUpdateModel.Position
            };
            var customerFeedbackUpdateResult = await _unitOfWork.customerFeedbackRepository.UpdateAsync(customerViewUpdateModelResult);
            if (!customerFeedbackUpdateResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "This data is null or empty");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion UPDATE

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

        #endregion DELETE
    }
}