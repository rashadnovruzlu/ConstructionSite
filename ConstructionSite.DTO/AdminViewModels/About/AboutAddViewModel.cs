using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.About
{
  public  class AboutAddViewModel
    {
        public int Id { get; set; }
		[Display(Name = "TittleAz")]
		[Required(ErrorMessage = "TittleAz is Required")]
        public string TittleAz { get; set; }

		[Display(Name = "TittleEn")]
		[Required(ErrorMessage = "TittleEN is Required")]
		public string TittleEn { get; set; }

		[Display(Name = "TittleRu")]
		[Required(ErrorMessage = "TittleRu is Required")]
		public string TittleRu { get; set; }
		[Required(ErrorMessage = "ContentAz is Required")]
		[Display(Name = "ContentAz")]
		
		public string ContentAz { get; set; }
		[Display(Name = "ContentEn")]
		[Required(ErrorMessage = "ContentEN is Required")]
		public string ContentEn { get; set; }
	    [Display(Name = "ContentRu")]
		[Required(ErrorMessage = "ContentRu is Required")]
		public string ContentRu { get; set; }
	}
}
