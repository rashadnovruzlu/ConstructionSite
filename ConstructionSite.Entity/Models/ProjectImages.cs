using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class ProjectImages
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsMain { get; set; }

        public Image Image { get; set; }
        [Required]
        public int ImageId { get; set; }

        public Project Project { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}
