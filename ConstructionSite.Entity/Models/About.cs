using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	   public class About: TitleContent
	   {
			public int Id { get; set; }
			public virtual ICollection<AboutImage> AboutImages { get; set; }
	   }
}