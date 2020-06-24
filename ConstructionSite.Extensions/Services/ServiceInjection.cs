using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.Services
{
    public static class ServiceInjection
    {
        public static void ServiceLoad(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<mycontext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),x=>x.MigrationsAssembly("ConstructionSite")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}