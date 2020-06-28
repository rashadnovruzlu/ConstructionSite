using ConstructionSite.Entity.Data;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.Services
{
    public static class ServiceInjectionExtensions
    {
        public static void ServiceLoad(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ConstructionDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("Nurana"),b=>b.MigrationsAssembly("ConstructionSite")));

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}