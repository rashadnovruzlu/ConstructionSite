using ConstructionSite.Entity.Data;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Implementations;
using ConstructionSite.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public static IServiceCollection ServiceDataBaseWithInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ConstructionDbContext>(options =>
            options.UseLazyLoadingProxies().
              UseSqlServer(Configuration.GetConnectionString("ConstructionSite"), b => b.MigrationsAssembly("ConstructionSite")));
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<ConstructionDbContext>());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            
            return services;
        }
    }
}