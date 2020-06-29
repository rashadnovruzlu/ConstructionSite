using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class SubServiceRepository : GenericRepository<SubService>, ISubServiceRepository
    {
        public SubServiceRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
}
