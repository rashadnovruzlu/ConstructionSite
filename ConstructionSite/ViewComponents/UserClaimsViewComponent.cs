﻿using ConstructionSite.DTO.AdminViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    [ViewComponent(Name = "Claims")]
    public class UserClaimsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
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
