using ConstructionSite.DTO.AdminViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConstructionSite.ViewComponents
{
    public class UserClaimsViewComponent : ViewComponent
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