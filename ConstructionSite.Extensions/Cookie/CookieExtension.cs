using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Extensions.Cookie
{
   public static  class CookieExtension
    {
        public static void CookiePath(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/ConstructionAdmin/Account/Login");
                options.AccessDeniedPath = new PathString("/ConstructionAdmin/Account/Index");

            });
            services.AddAuthentication(CookieAuthenticationDefaults
                        .AuthenticationScheme)
                            .AddCookie();
        }
    }
}
