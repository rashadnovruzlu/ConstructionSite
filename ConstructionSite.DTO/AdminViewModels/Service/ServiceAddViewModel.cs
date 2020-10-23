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

        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public int ImageId { get; set; }
        public ICollection<IFormFile> FileData { get; set; }
    }
}