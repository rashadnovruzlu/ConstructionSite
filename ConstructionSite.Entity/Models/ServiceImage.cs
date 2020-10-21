using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class ServiceImage
    {
        public int Id { get; set; }

        public virtual Service Service { get; set; }
        public int ServiceId { get; set; }

        public virtual Image Image { get; set; }
        public int ImageId { get; set; }
    }
}
