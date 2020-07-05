using ConstructionSite.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.Entity.Models
{
	public class Service
	{
	
		public int Id { get; set; }
		
		public string NameAz { get; set; }
	
		public string NameEn { get; set; }
	
		public string NameRu { get; set; }
		
		public string TittleAz { get; set; }
		public string TittleEn { get; set; }
		public string TittleRu { get; set; }
		
		public virtual Image Image { get; set; }
		public int ImageId { get; set; }

		public virtual ICollection<SubService> SubServices { get; set; }
		public virtual string FindTitle(string lang)
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
