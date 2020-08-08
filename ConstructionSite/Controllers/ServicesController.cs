using ConstructionSite.Cores.Queryes;
using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.DTO.FrontViewModels.SubService;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ServicesController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.getLanguages();
            _localizationHandle = localizationHandle;
        }

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var allServiceViewModelResult = _unitOfWork.ServiceRepository
               .GetAll()
               .Select(x => new ServiceViewModel
               {
                   Id = x.Id,
                   Name = x.FindName(_lang),
                   Tittle = x.FindName(_lang),
                   image = x.Image.Path
               })
               .ToList();
            if (allServiceViewModelResult == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(allServiceViewModelResult);
        }

        public IActionResult Services(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            if (id < 1)
            {
                return RedirectToAction("Index");
            }

            var serviceSubServiceImageResult = _unitOfWork
             .SubServiceImageRepositoryQuery(id, _lang);
            if (serviceSubServiceImageResult == null)
            {
                var singleServiceViewModelsResult =
                    _unitOfWork.ServiceRepositoryQuery(id, _lang);

                return View("Single", singleServiceViewModelsResult);
            }
            return View(serviceSubServiceImageResult);
        }

        public IActionResult SubService(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var serviceSubServiceResult = _unitOfWork.SubServiceImageRepository
                 .GetAll()
                 .Include(x => x.SubService.Service)
                 .Include(x => x.SubService)
                 .Include(x => x.SubService.SubServiceImages)
                 .Where(x => x.SubServiceId == id)
                 .Select(x => new ServiceSubServiceImage
                 {
                     id = x.Id,
                     SubServiceID = x.SubServiceId,
                     Content = x.SubService.FindContent(_lang),
                     Name = x.SubService.FindName(_lang),
                     Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()
                 })
                 .OrderByDescending(x => x.id)
                 .FirstOrDefault();
            if (serviceSubServiceResult == null)
            {
                return RedirectToAction("Index");
            }
            return View(serviceSubServiceResult);
        }
    }
}