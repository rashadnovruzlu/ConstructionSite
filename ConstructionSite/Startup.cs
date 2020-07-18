using ConstructionSite.Extensions.DataBase;
using ConstructionSite.Extensions.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConstructionSite.Extensions.Seed;
using Newtonsoft.Json;
using ConstructionSite.Helpers.Interfaces;
using ConstructionSite.Localization;

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
            services.AddLocalizationInjection();

            services.IdentityLoad(Configuration);
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
            services.ServiceDataBaseWithInjection(Configuration);
            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(ops => ops.LoginPath = "/ConstructionAdmin/Account/Login");
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = new PathString("/ConstructionAdmin/Account/Login");
            //    options.AccessDeniedPath = new PathString("/ConstructionAdmin/Dashboard/Index");
            //});
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

            app.UseStaticFiles();
            app.UseRequestLocalization();
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