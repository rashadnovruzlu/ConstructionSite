using ConstructionSite.Entity.Core;
using System.Collections.Generic;

namespace ConstructionSite.Entity.Models
{
    public class Galery : Title
    {
        public int Id { get; set; }
        public virtual ICollection<GaleryVido> GaleryVidos { get; set; }
        public virtual ICollection<GaleryFile> GaleryFiles { get; set; }
    }
}