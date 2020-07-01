using ConstructionSite.Entity.Data;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.DataBase
{
    public static class DataBaseInjectionExtension
    {
        public static void InjectionDataBase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ConstructionDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("ConstructionSite"), b => b.MigrationsAssembly("ConstructionSite")));
        }
        public static void ServiceDataBaseWithInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ConstructionDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("ConstructionSite"), b => b.MigrationsAssembly("ConstructionSite")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
