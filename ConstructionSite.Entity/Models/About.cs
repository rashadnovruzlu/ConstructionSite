using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class About: TitleContent
	   {
			public int Id { get; set; }
			public virtual ICollection<AboutImage> AboutImages { get; set; }
	   }
}