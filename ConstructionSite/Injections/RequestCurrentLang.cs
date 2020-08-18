using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
