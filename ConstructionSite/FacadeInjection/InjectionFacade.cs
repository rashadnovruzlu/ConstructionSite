using ConstructionSite.Facade.Galerys;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Interface.Facade.Service;
using Microsoft.Extensions.DependencyInjection;
using ConstructionSite.Facade.About;
using ConstructionSite.Facade.Portfolio;
using ConstructionSite.Facade.ServiceImages;
using ConstructionSite.Facade.Services;
using ConstructionSite.Interface.Facade.Servics;
using ConstructionSite.Interface.Facade.Services;

namespace ConstructionSite.FacadeInjection
{
    public static class InjectionFacade
    {
        public static void LoadFacade(this IServiceCollection services)
        {
            services.AddTransient<IAboutFacade, AboutFacade>();
            services.AddTransient<IGaleryFacade, GaleryFacade>();
            services.AddTransient<IGaleryFileFacde, GaleryFileFacde>();
            services.AddTransient<IAboutFacade, AboutFacade>();
            services.AddTransient<IPortfolioImageFacade, PortfolioImageFacade>();
            services.AddTransient<IServiceFacade, ServiceFacade>();
            services.AddTransient<IServiceImageFacade, ServiceImageFacade>();
            services.AddTransient<IServiceQueryFacade, ServiceQueryFacade>();
        }
    }
}
