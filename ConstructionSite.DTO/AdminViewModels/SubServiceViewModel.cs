using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels
{
   public class SubServiceViewModel
    {
        public string Name { get; set; }

        public string Content { get; set; }
        public ICollection<DescriptionViewModel> Descriptions { get; set; }

        public ICollection<SubServiceImage> SubServiceImages { get; set; }
    }
}
