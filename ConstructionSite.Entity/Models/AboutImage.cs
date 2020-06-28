using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class AboutImage
    {
        [Required]
        public int Id { get; set; }

        public About About { get; set; }
        [Required]
        public int AboutId { get; set; }
        
        public News Image { get; set; }
        [Required]
        public int ImageId { get; set; }
    }
}
