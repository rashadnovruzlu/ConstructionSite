using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class SubServiceImageRepository : GenericRepository<SubServiceImage>, ISubServiceImageRepository
    {
        public SubServiceImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
}
