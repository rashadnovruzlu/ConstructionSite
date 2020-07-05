using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels
{
  public  class ServiceMenuViewModel
    {
		public int Id { get; set; }

		public string Name { get; set; }

		
		public virtual ICollection<SubService> SubServices { get; set; }
	}
}
