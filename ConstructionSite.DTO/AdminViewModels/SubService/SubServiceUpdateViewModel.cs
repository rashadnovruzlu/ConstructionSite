using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.AdminViewModels.SubService
{
    public class SubServiceUpdateViewModel
    {
        public int Id { get; set; }
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }

     
        public int ServerId { get; set; }

        public List<int> ImageID { get; set; }
        public IList<IFormFile> files { get; set; }
        public List<data.Image> Images { get; set; }
    }
}