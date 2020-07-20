using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Project
{
   public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int ImageId { get; set; }
        public PortfolioViewModel Portfolio { get; set; }
        
    }
}
