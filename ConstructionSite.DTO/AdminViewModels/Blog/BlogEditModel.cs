using Microsoft.AspNetCore.Http;
using System;
using System.Collections;

namespace ConstructionSite.DTO.AdminViewModels.Blog
{
    public class BlogEditModel
    {
        public int Id { get; set; }
        public IFormFile file { get; set; }
        public string TittleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public string Image { get; set; }
        public int ImageId { get; set; }
        public DateTime DateTime { get; set; }
        public int NewsId { get; set; }
    }
}