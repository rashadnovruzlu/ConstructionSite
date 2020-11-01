using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Galery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class GaleryVideosViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IGaleryFileFacde _galeryFileFacde;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GaleryVideosViewComponent(IGaleryFileFacde galeryFileFacde,
                               IHttpContextAccessor httpContextAccessor)
        {
            _galeryFileFacde = galeryFileFacde;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.GetLanguages();
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            var result= _galeryFileFacde.GetAllVideo(_lang);
            return Task.FromResult<IViewComponentResult>(View(result));
        }
    }
}