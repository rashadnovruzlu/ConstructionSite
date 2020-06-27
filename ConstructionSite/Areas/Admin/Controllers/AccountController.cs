using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Areas.Admin.Models;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager)
        {
            this.userManager=userManager;
            this.SignInManager=SignInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logion(string returnUrl)
        {
            ViewBag.returnUrl= returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logion(LoginModel loginModel, string returnUrl)
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
