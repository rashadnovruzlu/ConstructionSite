using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class ProjectImageRepository : GenericRepository<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(ConstructionDbContext context) : base(context)
        {

        }


    }
}
