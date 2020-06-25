using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Service
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public ICollection<Description> Descriptions { get; set; }
	}
}
