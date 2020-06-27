using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Project
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
        [Required]
        public string ContentAz { get; set; }
        public string ContentRu { get; set; }
        public string ContentEn { get; set; }

        public Portfolio Portfolio { get; set; }
        [Required]
        public int PortfolioId { get; set; }

        public ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
