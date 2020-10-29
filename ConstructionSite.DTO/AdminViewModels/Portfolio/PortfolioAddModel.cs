using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.Portfolio
{
    public class PortfolioAddModel
    {

        public string NameAz { get; set; }

        public string NameEn { get; set; }

        // [UIHint]
        public string NameRu { get; set; }
        // public ICollection<IFormFile> formFile { get; set; }
    }
}