using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class HomePage
    {
        [Required]
        public int Id { get; set; }

        public Image Image { get; set; }
        public int ImageId { get; set; }
    }
}
