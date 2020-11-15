using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using data = ConstructionSite.ViwModel.FrontViewModels.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Slider
{
    public interface ISliderFacade
    {

        List<SliderViewModel> GetAll(string _lang);
        List<data.SliderViewModel> GetForSlider(string _lang);
        Task<RESULT<Sliders>> Add(SliderAddViewModel sliderAddViewModel);
        SliderUpdateViewModel GetUpdate(int id);
        Task<RESULT<Sliders>> Update(SliderUpdateViewModel sliderUpdateViewModel);
        bool Delete(int id);

    }
}
