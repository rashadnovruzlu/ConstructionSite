using ConstructionSite.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

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
            string languagesResult = _localizer[key].Value;
            if (!string.IsNullOrEmpty(languagesResult))
            {
                return languagesResult.ToUpper();
            }

            return key;
        }
    }
}