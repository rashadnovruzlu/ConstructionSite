using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class SliderImageRepostory : GenericRepository<SliderImage>, ISliderImageRepstory
    {
        public SliderImageRepostory(ConstructionDbContext context) : base(context)
        {
        }
    }
}