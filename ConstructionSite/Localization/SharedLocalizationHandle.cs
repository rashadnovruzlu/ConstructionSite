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
            var type = typeof(SharedResource);

            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            localizer = stringLocalizer.Create("SharedResource", assemblyName.Name);
        }
        public string GetLocalizationByKey(string key)
        {
            return localizer[key].Value;
        }
    }
}
