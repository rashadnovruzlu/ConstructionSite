using ConstructionSite.Entity.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class Galery : Title
    {
        public int Id { get; set; }
        public virtual ICollection<GaleryVido> GaleryVidos { get; set; }
        public virtual ICollection<GaleryFile> GaleryFiles { get; set; }
    }
}
