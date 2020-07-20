using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Project
{
  public  class ProjectUpdateViewModel
    {
        public int Id { get; set; }
        public string NameAz { get; set; }

        public string NameEn { get; set; }

        public string NameRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }
        public int PortfolioId { get; set; }
        public string ImagePath { get; set; }
    }
}
