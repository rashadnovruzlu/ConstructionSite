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
                    Email = "vn.nurlan@gmail.com",
                    UserName = "Const_Site",
                    PhoneNumber = "88(02) 123456",
                    Address = "1 Beverly Hills, Los Angeles, California, 90210, United States"
                };
                IdentityResult identityResult = db.CreateAsync(app, "Nurlan12345").GetAwaiter().GetResult();
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