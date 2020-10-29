using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class GaleryImagesViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}