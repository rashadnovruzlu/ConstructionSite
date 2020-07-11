using ConstructionSite.DTO.AdminViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    //[ViewComponent(Name = "UserProfileName")]
    public class UserProfileViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new UserDTO
            {
                Id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Name = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value,
                Role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}
