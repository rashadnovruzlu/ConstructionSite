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
        public static string getLang(this IHttpContextAccessor _httpContextAccessor)
        {
            var data = _httpContextAccessor.HttpContext.Request;

            var rqf = data.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
             return culture.Name;
        }
    }
}
