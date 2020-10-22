using ConstructionSite.Interface.Facade.Galery;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionSite.Extensions.ServiceFacade
{
    public static class FacadeInjection
    {
        public static void LoadFacade(this IServiceCollection services)
        {
            services.AddTransient<IGaleryFacade, GaleryFacade>();
        }
    }
}