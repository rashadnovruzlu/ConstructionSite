using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.About
{
  public  class AboutAddViewModel
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "TittleAz is Required")]
        public string TittleAz { get; set; }

		public string TittleEn { get; set; }

		public string TittleRu { get; set; }
		[Required(ErrorMessage = "ContentAz is Required")]
		public string ContentAz { get; set; }
		public string ContentEn { get; set; }
		public string ContentRu { get; set; }
	}
}
