using ConstructionSite.Helpers.Constants;
using ConstructionSite.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace ConstructionSite.Localization
{
    public static class LocalizationInjection
    {
        public static IServiceCollection AddLocalizationInjection(this IServiceCollection services)
        {
          
                
                services.AddLocalization(options => options.ResourcesPath = "Resources");

                services.AddSingleton<SharedLocalizationService>();

                services.Configure<RequestLocalizationOptions>(options =>
                {
                   
                    options.DefaultRequestCulture = new RequestCulture(LANGUAGECONSTANT.Az, LANGUAGECONSTANT.Az);

                  
                    options.SupportedCultures = LANGUAGECONSTANT.GetSupportedCulture();

                 
                    options.SupportedUICultures = LANGUAGECONSTANT.GetSupportedCulture();

                    //Added by Rashad
                    options.RequestCultureProviders = new List<IRequestCultureProvider>
                                                {
                                                    new QueryStringRequestCultureProvider(),
                                                    new CookieRequestCultureProvider()
                                                };
                });

                services.AddControllersWithViews()
                   
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                
                    .AddDataAnnotationsLocalization(options =>
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                        {
                            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName ?? string.Empty);
                            return factory.Create("SharedResource", assemblyName.Name);
                        };
                    });

                return services;
            }

            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            //services.AddSingleton<SharedLocalizationService>();

            //services.Configure<RequestLocalizationOptions>(options =>
            //{

            //    options.DefaultRequestCulture = new RequestCulture(LANGUAGECONSTANT.Az, LANGUAGECONSTANT.Az);


            //    options.SupportedCultures = LANGUAGECONSTANT.GetCultureInfo();


            //    options.SupportedUICultures = LANGUAGECONSTANT.GetCultureInfo();


            //    options.RequestCultureProviders = new List<IRequestCultureProvider>
            //                                    {
            //                                        new QueryStringRequestCultureProvider(),
            //                                        new CookieRequestCultureProvider()
            //                                    };
            //});

            //services.AddControllersWithViews()

            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)

            //    .AddDataAnnotationsLocalization(options =>
            //    {
            //        options.DataAnnotationLocalizerProvider = (type, factory) =>
            //        {
            //            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName ?? string.Empty);
            //            return factory.Create("SharedResource", assemblyName.Name);
            //        };
            //    });

            //return services;
        }
    }

