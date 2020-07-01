using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Areas.Admin.Models;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ConstructionSite.DTO;
using ConstructionSite.Areas.ConstructionAdmin.Models.DTO;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager)
        {
            this.userManager=userManager;
            this.SignInManager=SignInManager;
        }

        public IActionResult Index()
        {
            IEnumerable<UserDTO> users = userManager.Users.Select(m => new UserDTO
            {
                Id = m.Id,
                Name = m.Name,
                Email=m.Email
            });
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl= returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user=await userManager.FindByEmailAsync(loginModel.Email);
                if (user==null)
                {
                    var result=await SignInManager.PasswordSignInAsync(user,loginModel.Password,true,true);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl??"/");
                    }
                    else
                    {
                      ModelState.AddModelError("Description","Invalid Email or Password ");
                    }

                }
                if (user!=null)
                {

                }
            }
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
