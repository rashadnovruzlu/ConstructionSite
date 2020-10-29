using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using data = ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.AdminViewModels.Project
{
    public class ProjectUpdateViewModel
    {
        public int Id { get; set; }
        public string NameAz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }
        public int PortfolioId { get; set; }
        public int ProjectId { get; set; }
        public List<int> ImageID { get; set; }
        public IList<IFormFile> files { get; set; }
        public List<data.Image> Images { get; set; }
    }
}