using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.AdminViewModels.Blog
{
   public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Imagepath { get; set; }
        public int NewsId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
