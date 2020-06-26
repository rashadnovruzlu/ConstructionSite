using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class About
	{
		[Required]
		public int Id { get; set; }
		[Required]
		[MaxLength(250)]
		public string TittleAz { get; set; }
		[MaxLength(250)]
		public string TittleRu { get; set; }
		[MaxLength(250)]
		public string TittleEn { get; set; }
		[Required]
		public string ContentAz { get; set; }
		public string ContentRu { get; set; }
		public string ContentEn { get; set; }	
	}
}