using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}