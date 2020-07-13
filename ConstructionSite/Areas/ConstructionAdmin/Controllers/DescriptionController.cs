using ConstructionSite.DTO.AdminViewModels.Description;
using ConstructionSite.Entity.Models;
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
    [Authorize(Roles = "Admin")]
    public class DescriptionController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region CTOR
        public DescriptionController(IUnitOfWork unitOfWork,
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
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _unitOfWork.descriptionRepstory.GetAll()
                .Select(x => new DescriptionViewModel
                {
                    Id = x.Id,
                    Tittle = x.FindTitle(_lang),
                    Content = x.FindContent(_lang)
                }).ToList();
            if (result == null | result.Count == 0)
            {
                ModelState.AddModelError("", "Description is null or empty");
            }
            return View(result);
        }

        #endregion

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _unitOfWork.SubServiceRepository.GetAll()
                 .Select(x => new DescriptionSubServer
                 {
                     Id = x.Id,
                     Name = x.FindName(_lang)
                 }).ToList();
            if (result == null | result.Count < 0)
            {
                ModelState.AddModelError("", "Sub Service is empty.");
            }
            ViewBag.items = result;
            _unitOfWork.Dispose();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DescriptionAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (model == null)
            {
                ModelState.AddModelError("", "Description is empty.");
            }
            Description Descriptionresult = new Description
            {
                Id = model.Id,
                TittleAz = model.TittleAz,
                TittleRu = model.TittleRu,
                TittleEn = model.TittleEn,
                ContentAz = model.ContentAz,
                ContentRu = model.ContentRu,
                ContentEn = model.ContentEn,
                SubServiceId = model.SubServiceID
            };
            var isResult = await _unitOfWork.descriptionRepstory.AddAsync(Descriptionresult);
            if (isResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            var result = _unitOfWork.SubServiceRepository.GetAll()
                .Select(x => new DescriptionSubServer
                {
                    Id = x.Id,
                    Name = x.FindName(_lang)
                }).ToList();
            if (result == null | result.Count < 0)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Sub Service is empty.");
            }
            ViewBag.items = result;
            _unitOfWork.Dispose();
            return View();
        }

        #endregion

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id == 0)
            {
                ModelState.AddModelError("", "Id is NULL");
            }
            var descriptionUpdateViewModel = _unitOfWork.descriptionRepstory.GetById(id);
            if (descriptionUpdateViewModel == null)
            {
                ModelState.AddModelError("", "Description is empty");
            }
            var result = new DescriptionAddViewModel
            {
                Id = descriptionUpdateViewModel.Id,
                TittleAz = descriptionUpdateViewModel.TittleAz,
                TittleRu = descriptionUpdateViewModel.TittleRu,
                TittleEn = descriptionUpdateViewModel.TittleEn,
                ContentAz = descriptionUpdateViewModel.ContentAz,
                ContentRu = descriptionUpdateViewModel.ContentRu,
                ContentEn = descriptionUpdateViewModel.ContentEn,
                SubServiceID = descriptionUpdateViewModel.SubServiceId
            };
            _unitOfWork.Dispose();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DescriptionUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (model == null)
            {
                ModelState.AddModelError("", "Description is empty");
            }
            var DescriptionUpdateViewModel = new Description
            {
                Id = model.Id,
                TittleAz = model.TittleAz,
                TittleRu = model.TittleRu,
                TittleEn = model.TittleEn,
                ContentAz = model.ContentAz,
                ContentRu = model.ContentRu,
                ContentEn = model.ContentEn,
                SubServiceId = model.SubServiceID
            };
            var result = _unitOfWork.descriptionRepstory.Update(DescriptionUpdateViewModel);
            if (!result.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while editing Description.");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return View(model.Id);
        }

        #endregion

        #region DELETE

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid."); 
            }
            if (id == 0)
            {
                ModelState.AddModelError("", "NULL");
            }
            var resultbyId = _unitOfWork.descriptionRepstory.GetById(id);
            if (resultbyId == null)
            {
                ModelState.AddModelError("", "NULL");
            }
            var result = _unitOfWork.descriptionRepstory.Delete(resultbyId);
            if (!result.IsDone)
            {
                _unitOfWork.Rollback();
            }
            else
            {
                _unitOfWork.Rollback();
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return View(id);
        }

        #endregion
    }
}