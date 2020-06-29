using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Service
	{
		//[Required]
		public int Id { get; set; }
		//[Required]
		//[MaxLength(75)]
		public string NameAz { get; set; }
		//[MaxLength(75)]
		public string NameEn { get; set; }
		//[MaxLength(75)]
		public string NameRu { get; set; }
		//[Required]
		public string TittleAz { get; set; }
		public string TittleEn { get; set; }
		public string TittleRu { get; set; }
		
		public Image Image { get; set; }
		public int ImageId { get; set; }

		public ICollection<SubService> SubServices { get; set; }
	}
}
