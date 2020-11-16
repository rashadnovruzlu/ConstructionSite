using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.FrontViewModels.Slider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISliderFacade _sliderFacade;
        private readonly IUnitOfWork _unitOfWork;

        public SliderViewComponent(ISliderFacade sliderFacade, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }



        public IViewComponentResult Invoke()
        {
            var result = _unitOfWork.SliderRepostory
                .GetAll()
                .Select(x => new SliderViewModel
                {
                    Content = x.FindContent(_lang),
                    Tittle = x.FindTitle(_lang),
                    imagePath = x.ImagePath

                })
                .ToList();


            return View(result);
        }
    }
}
