using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.ViwModel.AdminViewModels.Galery
{
    public class GaleryUpdateViewModel
    {
        public int Id { get; set; }
        public string TitleAz { get; set; }

        public string TitleEn { get; set; }

        public string TitleRu { get; set; }
        public ICollection<IFormFile> files { get; set; }
    }
}
