using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}