using ConstructionSite.Entity.Core;

namespace ConstructionSite.Entity.Models
{
    public class Sliders : TitleContent
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        //  public virtual ICollection<SliderImage> SliderImages { get; set; }
    }
}