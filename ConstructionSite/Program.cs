using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace ConstructionSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                using (RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    if (roleManager.Roles.Count() == 0)
                    {
                        Task<IdentityResult> role = roleManager.CreateAsync(new IdentityRole
                        {
                            Name = "Admin"
                        });
                        role.Wait();
                    }
                }
                using (UserManager<ApplicationUser> db = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>())
                {
                    if (!db.Users.Any())
                    {
                        ApplicationUser app = new ApplicationUser
                        {
                            Name = "Construction Site",
                            Email = "nurane@pragmatech.az",
                            UserName = "Const_site"
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton(
                                        new ResourceManager("ConstructionSite.Resources.Controllers.HomeController",
                                            typeof(Startup).GetTypeInfo().Assembly));
                }).UseStartup<Startup>();
    }
}