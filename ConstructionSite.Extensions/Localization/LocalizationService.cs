using ConstructionSite.Helpers.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConstructionSite.Extensions.Localization
{
   public static class LocalizationService
    {
        public static void LocalizationLoad(IServiceCollection services)
        {
            services.AddLocalization(x => x.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            { 
            options.DefaultRequestCulture=new RequestCulture(LANGUAGECONSTANT.Az, LANGUAGECONSTANT.Az);
            options.SupportedCultures=LANGUAGECONSTANT.GetCultureInfo();
            options.SupportedUICultures=LANGUAGECONSTANT.GetCultureInfo();
            
            });
        }
    }
}
