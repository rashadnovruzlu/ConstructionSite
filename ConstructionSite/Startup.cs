using ConstructionSite.Extensions.DataBase;
using ConstructionSite.Extensions.Identity;
using ConstructionSite.Extensions.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ConstructionSite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        } 

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            ;
          
            services.IdentityLoad(Configuration);
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.ServiceDataBaseWithInjection(Configuration);
            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/ConstructionAdmin/Account/Login");
                options.AccessDeniedPath = new PathString("/ConstructionAdmin/Dashboard/Index");
            });
            services.AddAuthentication(CookieAuthenticationDefaults
                        .AuthenticationScheme)
                            .AddCookie();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
          //  app.SeedRole();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
          
        }
    }
}