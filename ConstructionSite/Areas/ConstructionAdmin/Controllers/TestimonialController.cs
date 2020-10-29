using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Testimonial;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class TestimonialController : CoreController
    {
        #region Fields

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly ITestimonialFacade _testimonialFacade;

        #endregion Fields

        #region CTOR

        public TestimonialController(IUnitOfWork unitOfWork,
                                     IWebHostEnvironment env,
                                     IHttpContextAccessor httpContextAccessor,
                                      ITestimonialFacade testimonialFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _testimonialFacade = testimonialFacade;
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
            var resultcustomerAddViewModel = await _testimonialFacade.Add(customerAddViewModel);
            if (resultcustomerAddViewModel.IsDone)
            {
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                return View();
            }

            return View();
        }

        #endregion CREATE

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            var customerFeedbackUpdate = _testimonialFacade.GetForUpdate(id);
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
        public IActionResult Update(CustomerViewUpdateModel customerViewUpdateModel)
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
            var resultUpdate = _testimonialFacade.Update(customerViewUpdateModel);
            if (resultUpdate)
            {
                if (_unitOfWork.Commit() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                    return View(customerViewUpdateModel);
                }
            }
            return View(customerViewUpdateModel);
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
            var resultDelete = _testimonialFacade.Delete(id);
            if (resultDelete)
            {
                if (_unitOfWork.Commit() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                    return View();
                }
            }
            return View();
        }

        #endregion DELETE
    }
}