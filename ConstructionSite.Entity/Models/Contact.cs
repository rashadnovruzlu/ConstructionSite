using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Contact
	{
		[Required]
		public int Id { get; set; }
		[Required]
		[MaxLength(150)]
		public string TittleAz { get; set; }
		[MaxLength(150)]
		public string TittleEn { get; set; }
		[MaxLength(150)]
		public string TittleRu { get; set; }
		[Required]
		public string ContentAz { get; set; }
		public string ContentEn { get; set; }
		public string ContentRu { get; set; }
		[Required]
		[MaxLength(150)]
		public string Address { get; set; }
		[Required]
		[MaxLength(150)]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
		public string Email { get; set; }
		[Required]
		[MaxLength(150)]
		[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
		public string PhoneNumber { get; set; }
	}
}
