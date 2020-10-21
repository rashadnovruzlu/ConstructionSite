using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class HomePageRepository : GenericRepository<HomePage>, IHomePageRepository
    {
        public HomePageRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}