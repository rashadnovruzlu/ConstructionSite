using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConstructionSite.Cores.HelperTag
{
    public static class LanguageActive
    {
        public static string LanguageA(this IHtmlHelper htmlHelper, string controller, string action, string name)
        {
        var xx= htmlHelper.ViewContext;
            var routeData = htmlHelper.ViewContext.RouteData;

            var _actionName = routeData.Values["action"].ToString();
            var _controllerName = routeData.Values["controller"].ToString();
          
           
            var returnActive = (controller == _controllerName && (action == _actionName  ));
            return returnActive ? "current-menu-item" : "";

        }
    }
}
