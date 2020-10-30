using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Galery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class GaleryController : Controller
    {
        private string _lang;
        private readonly IGaleryFileFacde _galeryFileFacde;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GaleryController(IGaleryFileFacde galeryFileFacde,
                                IHttpContextAccessor httpContextAccessor)
        {
            _galeryFileFacde = galeryFileFacde;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public IActionResult Index()
        {
            ViewBag.Images = _galeryFileFacde.GetAllImage(_lang);
            ViewBag.Videos = _galeryFileFacde.GetAllVideo(_lang);

            return View();
        }
    }
}