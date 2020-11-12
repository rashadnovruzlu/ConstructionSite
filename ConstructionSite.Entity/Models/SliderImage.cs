using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Entity.Models
{
    public class SliderImage
    {
        public int Id { get; set; }
        public virtual Sliders Sliders { get; set; }
        public virtual Image Image { get; set; }
        public virtual int SlidersId { get; set; }
        public virtual int ImageId { get; set; }
    }
}
