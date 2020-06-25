using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}