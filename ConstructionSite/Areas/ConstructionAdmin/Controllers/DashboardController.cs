using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(nameof(ConstructionAdmin))]
    public class DashboardController : Controller
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}