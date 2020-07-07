using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.DTO.AdminViewModels.SubService
{
    public class SubServiceViewModel
    {

        public string Name { get; set; }

        public string Content { get; set; }
        public ICollection<DescriptionViewModel> Descriptions { get; set; }

        public ICollection<SubServiceImage> SubServiceImages { get; set; }
    }
}
