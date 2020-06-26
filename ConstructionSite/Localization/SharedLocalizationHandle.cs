using ConstructionSite.Helpers.Core;
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
           string Name = "";///Tools.GetLocalizerPath<SharedResource>();
           localizer = stringLocalizer.Create("SharedResource", Name);
        }
        public string GetLocalizationByKey(string key)
        {
            return localizer[key].Value;
        }
    }
}
