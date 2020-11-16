using Microsoft.AspNetCore.Http;

namespace ConstructionSite.ViwModel.AdminViewModels.Slider
{
    public class SliderAddViewModel
    {
        public string TittleAz { get; set; }
        public string TittleRu { get; set; }
        public string TittleEn { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public string ImagePath { get; set; }
        public IFormFile file { get; set; }
    }
}