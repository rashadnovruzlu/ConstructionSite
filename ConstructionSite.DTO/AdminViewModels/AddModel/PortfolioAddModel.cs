using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.AddModel
{
   public class PortfolioAddModel
    {
        public string NameAz { get; set; }

        public string NameEn { get; set; }
        [UIHint]
        public string NameRu { get; set; }
    }
}
