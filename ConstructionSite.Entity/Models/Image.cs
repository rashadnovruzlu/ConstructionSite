using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Image
	{
		
		public int Id { get; set; }
		
		public string Title { get; set; }
		
		public string Path { get; set; }
		
		public virtual Service Service { get; set; }
        public virtual  About About { get; set; }
        public int AboutID { get; set; }
        public virtual SubService SubService { get; set; }
        public int SubServiceID { get; set; }

        public virtual ICollection<AboutImage> AboutImages { get; set; }

		public virtual ICollection<HomePage> HomePages { get; set; }

		public virtual ICollection<NewsImage> NewsImages { get; set; }

		public virtual ICollection<ProjectImage> ProjectImages { get; set; }

		public virtual ICollection<SubServiceImage> SubServiceImages { get; set; }
	}
}
