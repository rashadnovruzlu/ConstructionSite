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
             options.UseSqlServer(Configuration.GetConnectionString("Naib")));

            services.AddIdentity<ApplicationUser, IdentityRole>
                ().AddEntityFrameworkStores<ApplicationIdentityDbContext>
                ().AddDefaultTokenProviders();
        }
    }
}