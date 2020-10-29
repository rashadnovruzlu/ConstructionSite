using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.AdminViewModels.Portfolio
{
    public class PortfoliUpdateViewModel
    {
        public int id { get; set; }
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }
        public List<int> ImageID { get; set; }
        public IList<IFormFile> files { get; set; }
        public List<data.Image> Images { get; set; }
    }
}