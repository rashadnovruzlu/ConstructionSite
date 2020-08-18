using ConstructionSite.DTO.AdminViewModels.Account;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var admin = _userManager.Users.Select(x => new UserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email
            }).FirstOrDefault();
            return View(admin);
        }
    }
}