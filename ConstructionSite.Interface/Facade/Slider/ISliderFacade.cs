using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using System.Collections.Generic;
using System.Threading.Tasks;
using data = ConstructionSite.ViwModel.FrontViewModels.Slider;

namespace ConstructionSite.Interface.Facade.Slider
{
    public interface ISliderFacade
    {
        List<ConstructionSite.ViwModel.FrontViewModels.Slider.SliderViewModel> GetAll(string _lang);

        List<SliderViewModel> GetBackAll(string _lang);

        List<data.SliderViewModel> GetForSlider(string _lang);

        Task<RESULT<Sliders>> Add(SliderAddViewModel sliderAddViewModel);

        SliderUpdateViewModel GetUpdate(int id);

        Task<RESULT<Sliders>> Update(SliderUpdateViewModel sliderUpdateViewModel);

        bool Delete(int id);

        //Task<RESULT<Sliders>> Update(SliderUpdateViewModel sliderUpdateViewModel);
    }
}