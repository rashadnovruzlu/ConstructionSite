using ConstructionSite.Facade.Galerys;
using ConstructionSite.Interfaces.Facade;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.FacadeInjection
{
    public static class InjectionFacade
    {
        public static void LoadFacade(this IServiceCollection services)
        {
            services.AddTransient<IGaleryFacade, GaleryFacade>();
        }
    }
}
