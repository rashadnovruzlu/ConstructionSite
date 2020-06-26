using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class SubServiceImage
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsMain { get; set; }

        public Image Image { get; set; }
        [Required]
        public int ImageId { get; set; }

        public SubService SubService { get; set; }
        [Required]
        public int SubServiceId { get; set; }
    }
}
