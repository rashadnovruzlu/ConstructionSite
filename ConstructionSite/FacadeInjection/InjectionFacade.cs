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
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Facade.Blogs;
using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.Facade.Email;
using ConstructionSite.Facade.Projects;

namespace ConstructionSite.FacadeInjection
{
    public static class InjectionFacade
    {
        public static void LoadFacade(this IServiceCollection services)
        {
            services.AddTransient<IAboutFacade, AboutFacade>();
            services.AddTransient<IAboutImageFacade, AboutImageFacade>();
            services.AddTransient<IBlogFacade, BlogFacade>();
            services.AddTransient<IBlogImageFacade, BlogImageFacade>();
            services.AddTransient<IGaleryFacade, GaleryFacade>();
            services.AddTransient<IGaleryFileFacde, GaleryFileFacde>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPortfolioFacade, PortfolioFacade>();

            services.AddTransient<IBlogFacade, BlogFacade>();
            services.AddTransient<IBlogImageFacade, BlogImageFacade>();
            services.AddTransient<IBlogQueryFacade, BlogQueryFacade>();

            services.AddTransient<IPortfolioImageFacade, PortfolioImageFacade>();
            services.AddTransient<IServiceFacade, ServiceFacade>();
            services.AddTransient<IServiceImageFacade, ServiceImageFacade>();
            services.AddTransient<IProjectFacade, ProjectFacade>();
            services.AddTransient<IProjectImageFacade, ProjectImageFacade>();

            services.AddTransient<IPortfolioFacade, PortfolioFacade>();
            services.AddTransient<IServiceQueryFacade, ServiceQueryFacade>();
        }
    }
}
