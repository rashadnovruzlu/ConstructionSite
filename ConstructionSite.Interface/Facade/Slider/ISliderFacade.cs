using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using data= ConstructionSite.ViwModel.FrontViewModels.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Slider
{
    public interface ISliderFacade
    {
        List<data.SliderViewModel> GetAll(string _lang);
        Task<RESULT<Sliders>> Add(SliderAddViewModel sliderAddViewModel);
        SliderUpdateViewModel GetUpdate(int id);

    }
}
