using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Portfolio:Name
    {
        public int Id { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<PortfolioImage> PortfolioImages { get; set; }
    }
}
