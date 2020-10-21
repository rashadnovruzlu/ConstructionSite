using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// this is Main Page call some Components
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Soon()
        {
            return View();
        }
    }
}