using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class SliderController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISliderFacade _sliderFacade;

        public SliderController(IHttpContextAccessor httpContextAccessor, ISliderFacade sliderFacade)
        {
            _sliderFacade = sliderFacade;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}