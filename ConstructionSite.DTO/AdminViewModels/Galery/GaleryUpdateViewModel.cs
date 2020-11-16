using ConstructionSite.Entity.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.ViwModel.AdminViewModels.Galery
{
    public class GaleryUpdateViewModel
    {
        public int Id { get; set; }
        public string TitleAz { get; set; }

        public string TitleEn { get; set; }
        public string VidoPath { get; set; }
        public string TitleRu { get; set; }

        public List<int> ImageID { get; set; }
        public IList<IFormFile> files { get; set; }
        public List<Image> Images { get; set; }
    }
}