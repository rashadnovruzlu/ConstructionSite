﻿using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Contact:TitleContent
	{
	
		public int Id { get; set; }
		public string TittleAz { get; set; }
		public string TittleEn { get; set; }
		public string TittleRu { get; set; }
		public string ContentAz { get; set; }
		public string ContentEn { get; set; }
		public string ContentRu { get; set; }
		public string Address { get; set; }
		//[Required]
		//[MaxLength(150)]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
		public string Email { get; set; }
		//[Required]
		//[MaxLength(150)]
		[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
		public string PhoneNumber { get; set; }
	}
}
