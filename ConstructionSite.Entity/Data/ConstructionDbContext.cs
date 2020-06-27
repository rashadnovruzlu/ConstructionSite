using Microsoft.EntityFrameworkCore;

namespace ConstructionSite.Entity.Data
{
    public class ConstructionDbContext : DbContext
    {
        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options):base(options)
        {

        }
    }
}