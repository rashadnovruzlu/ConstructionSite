using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}