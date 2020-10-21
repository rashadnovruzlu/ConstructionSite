using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.ViewComponents
{
    public class LanguageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}