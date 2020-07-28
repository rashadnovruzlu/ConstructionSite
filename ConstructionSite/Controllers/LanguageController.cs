using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ConstructionSite.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            IRequestCultureFeature feature =
                HttpContext.Features.Get<IRequestCultureFeature>();

            // new request
            RequestCulture requestCulture =
                new RequestCulture(feature.RequestCulture.Culture,
                    new CultureInfo(culture));
            // create cookie
            string cookieValue =
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);
            // cookie name
            string cookieName =
                CookieRequestCultureProvider.DefaultCookieName;

            Response.Cookies.Append(cookieName, cookieValue);

            return LocalRedirect(returnUrl);
        }
    }
}