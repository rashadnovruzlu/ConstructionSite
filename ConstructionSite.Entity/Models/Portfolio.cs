using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Portfolio
    {
      
        public int Id { get; set; }
       
        public string NameAz { get; set; }
      
        public string NameEn { get; set; }
       
        public string NameRu { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
