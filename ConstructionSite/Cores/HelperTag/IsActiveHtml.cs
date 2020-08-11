using Microsoft.AspNetCore.Mvc.Rendering;


namespace ConstructionSite.Cores.HelperTag
{
    public static class IsActiveHtml
    {
        public static string Active(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var _actionName = routeData.Values["action"].ToString();
            var _controllerName = routeData.Values["controller"].ToString();

            var returnActive = (controller == _controllerName && (action == _actionName | _actionName == "Details" | _actionName == "Index"));


            return returnActive ? "current-menu-item" : "";
        }
        public static string Portfolio(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var _actionName = routeData.Values["action"].ToString();
            var _controllerName = routeData.Values["controller"].ToString();

            var returnActive = (controller == _controllerName && (action == _actionName | _actionName == "Details" | _actionName == "Index"));


            return returnActive ? "cbp-filter-item cbp-filter-item-active" : "";
        }
        public static string Active(this IHtmlHelper htmlHelper, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

           
            var _controllerName = routeData.Values["controller"].ToString();

            var returnActive = (controller ==_controllerName);


            return returnActive ? "current-menu-item" : "";
        }
    }
}
