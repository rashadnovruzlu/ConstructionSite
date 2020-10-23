using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class GaleryFile
    {
        public int Id { get; set; }
        //  public bool Type { get; set; }

        public virtual Galery Galery { get; set; }
        public virtual int GaleryId { get; set; }

        public virtual Image Image { get; set; }
        public virtual int ImageId { get; set; }

    }
}
