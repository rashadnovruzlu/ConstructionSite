using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class SubService:NameContent
    {
        public int Id { get; set; }
        
        public virtual Service Service { get; set; }
        
        public virtual int ServiceId { get; set; }
       
        public virtual ICollection<Description> Descriptions { get; set; }

        public virtual ICollection<SubServiceImage> SubServiceImages { get; set; }
    }
}
