using ConstructionSite.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ConstructionSite.Localization
{
    public class SharedLocalizationHandle
    {
        private readonly IStringLocalizer localizer;
        public SharedLocalizationHandle(IStringLocalizerFactory stringLocalizer)
        {
            

            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);

            localizer = stringLocalizer.Create("SharedResource", assemblyName.Name);
        }
        public string GetLocalizationByKey(string key)
        {
            return localizer[key].Value;
        }
    }
}
