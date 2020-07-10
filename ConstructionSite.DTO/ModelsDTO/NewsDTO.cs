using System;
using System.Collections.Generic;
using System.Text;
using ConstructionSite.Entity.Models;

namespace ConstructionSite.DTO.ModelsDTO
{
    public class NewsDTO
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
