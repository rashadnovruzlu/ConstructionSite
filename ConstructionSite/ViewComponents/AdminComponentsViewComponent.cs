using ConstructionSite.DTO.AdminViewModels.Account;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    [ViewComponent(Name ="AdminComponents")]
    public class AdminComponentsViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminComponentsViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = _userManager.Users.Select(m => new UserDTO
            {
                Id = m.Id,
                Name = m.Name,
                UserName = m.UserName,
                Email = m.Email
            }).ToList();

            return View(result);
        }
    }
}
