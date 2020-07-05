using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Seed
{
    public static class SeedRoleExtensions
    {
        public async static void SeedRole(this IApplicationBuilder builder)
        {
            RoleManager<IdentityRole> role = builder.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> db = builder.ApplicationServices.GetRequiredService<UserManager<ApplicationUser>>(); 
            if (role.Roles.Count() == 0)
            {
                var result = await role.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
            }

            if (!db.Users.Any())
            {
                ApplicationUser app = new ApplicationUser
                {
                    Name = "Construction Site",
                    Email = "nurane@pragmatech.az"
                };
                IdentityResult identityResult = db.CreateAsync(app, "Nurane_29").GetAwaiter().GetResult();
                if (identityResult.Succeeded)
                {
                    Task<IdentityResult> res = db.AddToRoleAsync(app, "Admin");
                    res.Wait();
                }
                else
                {
                    IEnumerable<IdentityError> identityErrors = identityResult.Errors;
                }
            }
        }
    }
}