using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Portfolio
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string NameAz { get; set; }
        [MaxLength(75)]
        public string NameEn { get; set; }
        [MaxLength(75)]
        public string NameRu { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
