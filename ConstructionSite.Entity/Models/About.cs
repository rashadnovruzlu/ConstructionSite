using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class About
	{
		
		public int Id { get; set; }
		public string TittleAz { get; set; }
		public string TittleRu { get; set; }
		public string TittleEn { get; set; }
		public string ContentAz { get; set; }
		public string ContentRu { get; set; }
		public string ContentEn { get; set; }	

		public virtual ICollection<AboutImage> AboutImages { get; set; }
	}
}