using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

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

        public ICollection<IFormFile> file { get; set; }
    }
}
