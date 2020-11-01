using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.AdminViewModels.About
{
    public class AboutUpdateViewModel
    {
        public int Id { get; set; }

        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public List<int> ImageID { get; set; }
        public IList<IFormFile> files { get; set; }
        public List<data.Image> Images { get; set; }
    }
}