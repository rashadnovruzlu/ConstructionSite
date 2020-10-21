using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class GaleryFile
    {
        public int Id { get; set; }
        //  public bool Type { get; set; }

        public Galery Galery { get; set; }
        public int GaleryId { get; set; }

        public Image Image { get; set; }
        public int ImageId { get; set; }
    }
}
