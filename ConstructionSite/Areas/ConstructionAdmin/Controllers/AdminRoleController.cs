using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager=roleManager;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (string.IsNullOrEmpty(Name))
            {
                ModelState.AddModelError("","data is null");
                return RedirectToAction("Index");
            }
            var identityRoleResult=await _roleManager.CreateAsync(new IdentityRole(Name));
            if (identityRoleResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in identityRoleResult.Errors)
                {
                    ModelState.AddModelError("", item.Description.ToString());
                }
                
            }
           
            return View(Name);
        }
    }
}
