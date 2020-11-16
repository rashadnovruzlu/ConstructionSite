using System.Collections.Generic;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SliderViewModel = ConstructionSite.ViwModel.FrontViewModels.Slider.SliderViewModel;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISliderFacade _sliderFacade;
        public HomeController(ISliderFacade sliderFacade, IHttpContextAccessor httpContextAccessor)
        {
            _sliderFacade = sliderFacade;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public IActionResult Index()
        {
            //   List<ConstructionSite.ViwModel.FrontViewModels.Slider.SliderViewModel> sliderViewModels = new List<SliderViewModel>();
            //sliderViewModels = _sliderFacade.GetForSlider(_lang);
            ViewBag.sliderViewModels = _sliderFacade.GetAll(_lang);
            return View();
        }

        public IActionResult Soon()
        {
            return View();
        }
    }
}