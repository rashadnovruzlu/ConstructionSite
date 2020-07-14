using ConstructionSite.Entity.Core;
using DocumentFormat.OpenXml.Office2010.Drawing;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class About: TitleContent
	   {
		public int Id { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<AboutImage> AboutImages { get; set; }
	   }
}