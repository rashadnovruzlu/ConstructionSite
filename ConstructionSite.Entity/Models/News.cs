using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class News: TitleContent
    {
       
        public int Id { get; set; }
       
        public DateTime CreateDate { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; }
    }
}
