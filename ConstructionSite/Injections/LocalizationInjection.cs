using ConstructionSite.Helpers.Constants;
using ConstructionSite.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ConstructionSite.Injections
{
    public static class LocalizationInjection
    {
        public static void LocalizationLoad(IServiceCollection services)
        {
            services.AddLocalization(x => x.ResourcesPath = "Resources");
            services.AddSingleton<SharedLocalizationHandle>();
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
}
