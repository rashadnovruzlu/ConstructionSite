using ConstructionSite.Interfaces.Facade;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
