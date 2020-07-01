using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class AboutImage
    {
        //[Required]
        public int Id { get; set; }

        public About About { get; set; }
       
        public int AboutId { get; set; }
        
        public Image Image { get; set; }
       
        public int ImageId { get; set; }
    }
}
