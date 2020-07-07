using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels
{
  public  class PortfolioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectViewModel> ProjectViewModel { get; set; }
    }
}
