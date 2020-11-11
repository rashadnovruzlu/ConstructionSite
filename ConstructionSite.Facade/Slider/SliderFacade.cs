using ConstructionSite.Entity.Models;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Slider
{
    public class SliderFacade : ISliderFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public SliderFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> Add(SliderAddViewModel sliderAddViewModel)
        {

            var resultSliderAddViewModel = new Sliders
            {
                TittleAz = sliderAddViewModel.TittleAz,
                TittleEn = sliderAddViewModel.TittleEn,
                TittleRu = sliderAddViewModel.TittleRu,
                ContentAz = sliderAddViewModel.ContentAz,
                ContentEn = sliderAddViewModel.ContentEn,
                ContentRu = sliderAddViewModel.ContentRu,


            };
            _unitOfWork.SliderRepostory.AddAsync(sliderAddViewModel);
        }
    }
}
