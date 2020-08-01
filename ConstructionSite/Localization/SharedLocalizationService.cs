using ConstructionSite.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConstructionSite.Localization
{
    public class SharedLocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public SharedLocalizationService(IStringLocalizerFactory factory)
        {

            var type = typeof(SharedResource);

            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public string GetLocalizedHtmlString(string key)
        {
          
            string Result = _localizer[key].Value;
            if (!string.IsNullOrEmpty(Result))
            {
                return Result;
            }
            return key;
           
        }
    }
}
