using ConstructionSite.DTO.FrontViewModels.Testimonial;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class GaleryViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IGaleryFileFacde _galeryFileFacde;

        public GaleryViewComponent(IGaleryFileFacde galeryFileFacde,
                                         IHttpContextAccessor httpContextAccessor)
        {

            _galeryFileFacde = galeryFileFacde;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }

        public IViewComponentResult Invoke()
        {
            var resultVido = _galeryFileFacde.GetAllVideo(_lang);
            return View(resultVido);
        }
    }
}