using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class GaleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}