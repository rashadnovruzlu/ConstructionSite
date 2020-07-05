using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Project:NameContent
    {
       
        public int Id { get; set; }
       
      

        public virtual Portfolio Portfolio { get; set; }
       
        public int PortfolioId { get; set; }

        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
