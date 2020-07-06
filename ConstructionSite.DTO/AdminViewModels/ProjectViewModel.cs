using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels
{
   public class ProjectViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public PortfolioViewModel Portfolio { get; set; }
        
    }
}
