using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.FrontViewModels.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public int NewsId { get; set; }
        public string Image { get; set; }
        public int ImageId { get; set; }
    }
}
