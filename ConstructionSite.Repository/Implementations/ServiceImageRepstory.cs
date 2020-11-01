using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class ServiceImageRepstory : GenericRepository<ServiceImage>, IServiceImageRepstory
    {
        public ServiceImageRepstory(ConstructionDbContext context) : base(context)
        {
        }
    }
}