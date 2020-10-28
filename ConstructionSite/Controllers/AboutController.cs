using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private SharedLocalizationService _localizationHandle;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAboutFacade _aboutFacade;

        /// <summary>
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="localizationHandle"></param>
        /// <param name="httpContextAccessor"></param>
        public AboutController(IUnitOfWork unitOfWork,
                               SharedLocalizationService localizationHandle,
                               IHttpContextAccessor httpContextAccessor,
                               IAboutFacade aboutFacade)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
            _lang = _httpContextAccessor.GetLanguages();
            _aboutFacade = aboutFacade;
        }

        /// <summary>
        /// this is About
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var aboutImageViewResult = _aboutFacade.GetAll(_lang)
                .FirstOrDefault();
            if (aboutImageViewResult == null)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
                return RedirectToAction("Index", "Home");
            }
            return View(aboutImageViewResult);
        }
    }
}