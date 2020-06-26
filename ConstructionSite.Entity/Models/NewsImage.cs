using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class NewsImage
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsMain { get; set; }

        public News News { get; set; }
        [Required]
        public int NewsId { get; set; }

        public Image Image { get; set; }
        [Required]
        public int ImageId { get; set; }
    }
}
