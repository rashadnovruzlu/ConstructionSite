using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class GaleryVidoResptory : GenericRepository<GaleryVido>, IGaleryVidoResptory
    {
        public GaleryVidoResptory(ConstructionDbContext context) : base(context)
        {
        }
    }

    public class AboutImageRepository : GenericRepository<AboutImage>, IAboutImageRepository
    {
        public AboutImageRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}