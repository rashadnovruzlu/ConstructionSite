using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Description
{
   public class DescriptionAddViewModel
    {
        public int Id { get; set; }
        public string TittleAz { get; set; }

		public string TittleEn { get; set; }

		public string TittleRu { get; set; }

		public string ContentAz { get; set; }
		public string ContentEn { get; set; }
		public string ContentRu { get; set; }
        public int SubServiceID { get; set; }
    }
}
