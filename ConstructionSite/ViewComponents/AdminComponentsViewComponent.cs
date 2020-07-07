using ConstructionSite.Areas.ConstructionAdmin.Models.DTO;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

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
