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
            var httpContext= _httpContextAccessor.HttpContext;
            var data =httpContext.Request;
            var nes=httpContext.Response;

            var rqf = data.HttpContext.Features.Get<IRequestCultureFeature>();
            //var culture = rqf.RequestCulture.Culture;
            var culture = rqf.RequestCulture.UICulture;
            return culture.Name;
        }
    }
}
