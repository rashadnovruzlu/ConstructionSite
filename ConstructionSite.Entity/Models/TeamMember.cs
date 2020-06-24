using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class TeamMember
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

		public Image Image { get; set; }
		public int ImageId { get; set; }

		public int PositionId { get; set;  }
	}
}

