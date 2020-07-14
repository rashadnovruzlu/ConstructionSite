using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
   public class Description:TitleContent
    {
        [Key]
        public int Id { get; set; }
       
        public virtual SubService SubService { get; set; }
        public virtual int SubServiceId { get; set; }
       
    }
}
