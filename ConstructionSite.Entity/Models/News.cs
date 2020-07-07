using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class News: TitleContent
    {
        [Required]
        public int Id { get; set; }
        ////[Required]
        ////[MaxLength(150)]
        //public string TittleAz { get; set; }
        ////[MaxLength(150)]
        //public string TittleEn { get; set; }
        ////[MaxLength(150)]
        //public string TittleRu { get; set; }
        ////[Required]
        //public string ContentAz { get; set; }
        //public string ContentEn { get; set; }
        //public string ContentRu { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; }
    }
}
