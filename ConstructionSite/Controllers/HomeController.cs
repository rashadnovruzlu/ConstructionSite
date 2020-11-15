using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        //private string _lang;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ISliderFacade _sliderFacade;
        public HomeController()
        {
            //_sliderFacade = sliderFacade;
            //_httpContextAccessor = httpContextAccessor;
            //_lang = _httpContextAccessor.GetLanguages();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Soon()
        {
            return View();
        }
    }
}