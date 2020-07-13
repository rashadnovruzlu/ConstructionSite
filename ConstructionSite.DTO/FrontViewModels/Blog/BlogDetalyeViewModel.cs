using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels.Blog
{
   public class BlogDetalyeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string imagePath { get; set; }
        public DateTime dateTime { get; set; }
    }
}
