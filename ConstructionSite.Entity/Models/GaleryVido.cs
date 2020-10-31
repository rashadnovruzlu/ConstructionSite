using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class GaleryVido
    {
        public int Id { get; set; }
        public string VidoPath { get; set; }
        public virtual Galery Galery { get; set; }
        public int GaleryId { get; set; }
    }
}
