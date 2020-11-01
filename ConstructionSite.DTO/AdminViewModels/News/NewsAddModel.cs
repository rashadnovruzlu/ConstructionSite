using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ConstructionSite.DTO.AdminViewModels.News
{
    public class NewsAddModel
    {
        public int Id { get; set; }
        public string TittleAz { get; set; }

        public string TittleEn { get; set; }

        public string TittleRu { get; set; }

        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public IList<IFormFile> file { get; set; }
        public DateTime CreateDate { get; set; }
    }
}