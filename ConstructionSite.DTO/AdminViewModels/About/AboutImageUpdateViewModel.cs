using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.ViwModel.AdminViewModels.About
{
    public class AboutImageUpdateViewModel
    {
        public int Id { get; set; }

        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public ICollection<IFormFile> files { get; set; }
        public virtual int AboutId { get; set; }
        public virtual int ImageId { get; set; }
    }
}