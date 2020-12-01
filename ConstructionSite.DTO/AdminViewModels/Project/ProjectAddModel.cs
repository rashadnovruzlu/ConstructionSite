using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.Project
{
    public class ProjectAddModel
    {
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public List<IFormFile> files { get; set; }
        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }
        public int PortfolioId { get; set; }
    }
}