using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}