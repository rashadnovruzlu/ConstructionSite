﻿using ConstructionSite.Areas.ConstructionAdmin.Models.DTO;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    [ViewComponent(Name="AdminEdit")]
    public class AdminEditViewComponents : ViewComponent
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
