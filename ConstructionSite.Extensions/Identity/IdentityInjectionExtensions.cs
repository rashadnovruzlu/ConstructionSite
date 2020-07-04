using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.Identity
{
    public static class IdentityInjectionExtensions
    {
        public static void IdentityLoad(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("ConstructionSite"), b => b.MigrationsAssembly("ConstructionSite")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 7;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<ApplicationIdentityDbContext>
                ().AddDefaultTokenProviders();
        }
    }
}