using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class StaticFieldRepository : GenericRepository<StaticField>, IStaticFieldRepository
    {
        public StaticFieldRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}