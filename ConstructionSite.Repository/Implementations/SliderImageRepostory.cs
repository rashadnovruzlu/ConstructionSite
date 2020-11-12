using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Implementations
{
    public class SliderImageRepostory :GenericRepository<SliderImage>,ISliderImageRepstory
    {
        public SliderImageRepostory(ConstructionDbContext context):base(context)
        {

        }
      
    }
}
