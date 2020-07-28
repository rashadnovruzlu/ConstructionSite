using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private SharedLocalizationService _localizationHandle;

        public HomeController(IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor,
                              SharedLocalizationService localizationHandle
                                 )
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _localizationHandle = localizationHandle;
            _lang = _httpContextAccessor.getLang();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}