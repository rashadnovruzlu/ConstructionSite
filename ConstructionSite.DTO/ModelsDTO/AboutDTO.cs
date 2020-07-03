using ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.DTO.ModelsDTO
{
   public class AboutDTO
    {
        public int Id { get; set; }
        public string TitleAz { get; set; }
        public string TittleEn { get; set; }
        public string TittleRu { get; set; }
        public string ContentAz { get; set; }
        public string ContentEn { get; set; }
        public string ContentRu { get; set; }
        public string image { get; set; }
    }
}
