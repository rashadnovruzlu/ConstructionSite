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
		public virtual Image Image { get; set; }
		public int ImageId { get; set; }

		public virtual ICollection<SubService> SubServices { get; set; }
		public  string FindTitle(string lang)
        {
            if (lang== LANGUAGECONSTANT.Az)
            {
				return TittleAz;
            }
            else if(lang == LANGUAGECONSTANT.Ru)
            {
				return TittleRu;
            }
			return TittleEn;
        }
	}
}
