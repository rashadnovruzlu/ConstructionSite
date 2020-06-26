using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class CustomerFeedback
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string ContentAz { get; set; }
        [MaxLength(500)]
        public string ContentEn { get; set; }
        [MaxLength(500)]
        public string ContentRu { get; set; }
        [Required]
        [MaxLength(35)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

    }
}
