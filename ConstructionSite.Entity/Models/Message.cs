using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    class Message
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(2000)]
        public string UserMessage { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}{1:HH/mm}")]
        public DateTime SendDate { get; set; }
        [Required]
        [RegularExpression("False")]
        public bool IsAnswerd { get; set; }
    }
}
