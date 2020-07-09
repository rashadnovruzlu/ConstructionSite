using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.About
{
   public class AboutViewModel
    {
       
        public int Id { get; set; }
       
        public string Tittle { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int    imageId { get; set; }
        public int aboutID { get; set; }
       
    }
}
