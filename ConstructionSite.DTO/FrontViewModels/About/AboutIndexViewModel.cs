using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels.About
{
   public class AboutIndexViewModel
    {
        public int Id { get; set; }
        public int AboutID { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string  imagePath { get; set; }
        public List<string> path { get; set; }
    }
}
