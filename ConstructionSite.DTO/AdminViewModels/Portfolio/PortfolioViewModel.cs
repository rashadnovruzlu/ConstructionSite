using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.DTO.AdminViewModels.Portfolio
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectViewModel> ProjectViewModel { get; set; }
    }
}
