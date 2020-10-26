using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.ViwModel.AdminViewModels.Galery
{
    public class GaleryAddViewModel
    {
        public int Id { get; set; }
        public string TitleAz { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }
        public ICollection<IFormFile> files { get; set; }
    }
}
