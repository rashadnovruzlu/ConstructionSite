using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels
{
	public class ServiceViewModel
	{
		public int Id { get; set; }

		public string NameAz { get; set; }

		public string NameEn { get; set; }

		public string NameRu { get; set; }

		public string TittleAz { get; set; }
		public string TittleEn { get; set; }
		public string TittleRu { get; set; }
		public virtual ICollection<SubService> SubServices { get; set; }
		public string image { get; set; }
	}
}
