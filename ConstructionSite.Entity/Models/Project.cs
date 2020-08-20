using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Project : NameContent
    {
        public int Id { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public int PortfolioId { get; set; }

        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}