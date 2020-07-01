using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
}
