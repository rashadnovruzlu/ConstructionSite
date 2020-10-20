using ConstructionSite.Entity.Core;
using ConstructionSite.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Service:NameTitle
	{
	
		public int Id { get; set; }		
		
		public virtual ICollection<ServiceImage> ServiceImages { get; set; }
		public virtual ICollection<SubService> SubServices { get; set; }
		
	}
}
