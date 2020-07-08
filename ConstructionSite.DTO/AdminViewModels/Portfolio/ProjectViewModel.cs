using ConstructionSite.DTO.AdminViewModels.Portfolio;

namespace ConstructionSite.DTO.AdminViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public PortfolioViewModel Portfolio { get; set; }
        
    }
}
