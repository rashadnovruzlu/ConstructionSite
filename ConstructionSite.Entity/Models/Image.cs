using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Image
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string BigPath { get; set; }
		public string SmallPath { get; set; }
		public DateTime AddedDate { get; set; }

		public About About { get; set; }
		public int AboutId { get; set; }

		public TeamMember TeamMember { get; set; }
		public int TeamMemberId { get; set; }



	}
}
