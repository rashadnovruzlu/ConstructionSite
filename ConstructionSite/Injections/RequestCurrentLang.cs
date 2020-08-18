using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace ConstructionSite.Injections
{
    public static class RequestCurrentLang
    {
        public static string GetLanguages(this IHttpContextAccessor _httpContextAccessor)
        {
            var data = _httpContextAccessor.HttpContext.Request;
            var requestCultureFeature = data.HttpContext.Features.Get<IRequestCultureFeature>();
            var requestCulture = requestCultureFeature.RequestCulture.UICulture;
            return requestCulture.Name;
        }
    }
}