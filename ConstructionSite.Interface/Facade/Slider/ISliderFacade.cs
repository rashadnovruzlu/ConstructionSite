using ConstructionSite.ViwModel.AdminViewModels.Slider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Slider
{
    public interface ISliderFacade
    {
       Task<bool> Add(SliderAddViewModel sliderAddViewModel);
        
    }
}
