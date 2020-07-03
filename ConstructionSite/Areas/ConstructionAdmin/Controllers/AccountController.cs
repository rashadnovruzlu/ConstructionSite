using ConstructionSite.Areas.Admin.Models;
using ConstructionSite.Areas.ConstructionAdmin.Models.DTO;
using ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager=userManager;
            this.SignInManager=SignInManager;
            this._roleManager = roleManager;
        }

        [HttpGet]
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
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new UserViewModel
            {
                Roles = roles
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = viewModel.Username,
                        Name = viewModel.Name,
                        Email = viewModel.Email
                    };

                    IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

                    if(result.Succeeded)
                    {
                        ApplicationUser appUser = await userManager.FindByNameAsync(user.UserName);
                        IdentityRole role = await _roleManager.FindByIdAsync(viewModel.RoleId);
                        IdentityResult identityResult = await userManager.AddToRoleAsync(appUser, role.Name);

                        if (identityResult.Succeeded)
                            return RedirectToAction(nameof(Index));
                        else
                        {
                            AddErrors(result);

                            if (ModelState.ErrorCount != 0)
                            {
                                viewModel.Roles = await _roleManager.Roles.ToListAsync();
                                return View(viewModel);
                            }
                        }
                    }
                    else
                    {
                        AddErrors(result);

                        if (ModelState.ErrorCount != 0)
                        {
                            viewModel.Roles = await _roleManager.Roles.ToListAsync();
                            return View(viewModel);
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Some error occured. Please try again.");
                }
            }
            viewModel.Roles = await _roleManager.Roles.ToListAsync();
            return View(viewModel);
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
