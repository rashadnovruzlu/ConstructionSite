using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.Portfolio
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProjectViewModel> ProjectViewModel { get; set; }
    }
}
