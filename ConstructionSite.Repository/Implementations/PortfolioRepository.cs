using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
}
