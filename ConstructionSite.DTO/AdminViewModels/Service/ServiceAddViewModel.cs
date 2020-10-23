using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.Service
{
    public class ServiceAddViewModel
    {
        public int ID { get; set; }
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }

        public string TitleAz { get; set; }
        public string TitleEn { get; set; }
        public string TitleRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }
        public int ImageId { get; set; }
        public ICollection<IFormFile> FileData { get; set; }
    }
}