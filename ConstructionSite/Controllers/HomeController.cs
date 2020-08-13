using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.active = "current-menu-item";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}