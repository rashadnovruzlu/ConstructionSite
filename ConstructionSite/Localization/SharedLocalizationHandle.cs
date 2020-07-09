using ConstructionSite.Helpers.Interfaces;
using ConstructionSite.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ConstructionSite.Localization
{
    public class SharedLocalizationHandle: ISharedLocalizationHandle
    {
        private readonly IStringLocalizer localizer;
        public SharedLocalizationHandle(IStringLocalizerFactory stringLocalizer)
        {
            var Name = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName).Name;
            
           localizer = stringLocalizer.Create("SharedResource", Name);
        }
        public string GetLocalizationByKey(string key)
        {
            return localizer[key].Value;
        }
    }
}
