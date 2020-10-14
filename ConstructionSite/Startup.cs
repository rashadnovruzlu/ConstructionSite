using ConstructionSite.Extensions.DataBase;
using ConstructionSite.Extensions.Identity;
using ConstructionSite.Extensions.Seed;
using ConstructionSite.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;

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

            services.AddMvc();
            services.Localization();

            services.IdentityLoad(Configuration);

            services.ServiceDataBaseWithInjection(Configuration);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/ConstructionAdmin/Account/Login");
                options.AccessDeniedPath = new PathString("/ConstructionAdmin/Account/Index");

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


            app.SeedRole();
            app.SeedData();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRequestLocalization();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Account}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Soon}/{id?}");

            });

        }
    }
}