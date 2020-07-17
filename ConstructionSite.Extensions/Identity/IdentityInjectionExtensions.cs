using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                    //options.Password.RequiredLength = 7;
                    //options.Password.RequireDigit = false;
                    //options.Password.RequireLowercase = false;
                    //options.Password.RequireUppercase = false;
                    //options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase =false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;


                    // User settings.
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                    options.User.RequireUniqueEmail = false;
                }).AddEntityFrameworkStores<ApplicationIdentityDbContext>
                ().AddDefaultTokenProviders();
        }
    }
}