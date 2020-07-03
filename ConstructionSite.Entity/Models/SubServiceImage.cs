using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class SubServiceImage
    {
        
        public int Id { get; set; }
      
        public bool IsMain { get; set; }

        public Image Image { get; set; }
       
        public int ImageId { get; set; }

        public SubService SubService { get; set; }
        
        public int SubServiceId { get; set; }
    }
}
