using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
   public class Description:TitleContent
    {
        public int Id { get; set; }
       
        
        public virtual SubService SubService { get; set; }
        public virtual int SubServiceId { get; set; }
       
    }
}
