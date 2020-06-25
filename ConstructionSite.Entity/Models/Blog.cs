using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime AddedDate { get; set; }
	}
}