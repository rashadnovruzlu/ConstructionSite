using System;
using System.Collections.Generic;
using System.Text;
using ConstructionSite.Entity.Core;

namespace ConstructionSite.Entity.Models
{
    public class Slide : Title
    {
        public int Id { get; set; }
        public string Img { get; set; }
    }
}
