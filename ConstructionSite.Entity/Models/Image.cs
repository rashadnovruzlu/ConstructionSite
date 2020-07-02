using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Image
	{
		//[Required]
		public int Id { get; set; }
		//[MaxLength(150)]
		public string Title { get; set; }
		//[Required]
		//[MaxLength(250)]
		public string Path { get; set; }
		
		public Service Service { get; set; }
		

		public ICollection<AboutImage> AboutImages { get; set; }

		public ICollection<HomePage> HomePages { get; set; }

		public ICollection<NewsImage> NewsImages { get; set; }

		public ICollection<ProjectImage> ProjectImages { get; set; }

		public ICollection<SubServiceImage> SubServiceImages { get; set; }
	}
}
