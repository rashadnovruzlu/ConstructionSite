using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Project
    {
       
        public int Id { get; set; }
       
        public string NameAz { get; set; }
      
        public string NameEn { get; set; }
       
        public string NameRu { get; set; }
      
        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }

        public virtual Portfolio Portfolio { get; set; }
       
        public int PortfolioId { get; set; }

        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
