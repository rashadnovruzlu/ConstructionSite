using ConstructionSite.Helpers.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Injections
{
    public static class LocalizationInjection
    {
        public static void LocalizationLoad(IServiceCollection services)
        {
            services.AddLocalization(x => x.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                //hec bir dil göstərilməzsə default olaraq Azərbaycan dilini götür
                options.DefaultRequestCulture = new RequestCulture(LANGUAGECONSTANT.Az, LANGUAGECONSTANT.Az);
                //vaxt və tarix, pul formatı , rəqəm təsviri üçün
                options.SupportedCultures = LANGUAGECONSTANT.GetCultureInfo();
                //səhifədəki sözlərin tərcümə olunması üçün
                options.SupportedUICultures = LANGUAGECONSTANT.GetCultureInfo();
                //Naib Residov
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                                                {
                                                    new QueryStringRequestCultureProvider(),
                                                    new CookieRequestCultureProvider()
                                                };

            });
        }
}
