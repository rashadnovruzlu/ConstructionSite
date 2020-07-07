﻿using ConstructionSite.Areas.ConstructionAdmin.Models.DTO;
using ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels;
using ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels.Account;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
      //  private readonly ApplicationIdentityDbContext _identityDb;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public AccountController(ApplicationIdentityDbContext identityDb, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            this.userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
           // this._identityDb = identityDb;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<UserDTO> users = userManager.Users.Select(m => new UserDTO
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                UserName=m.UserName
            });
            return View(users);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = viewModel.Username,
                        Email = viewModel.Email,
                        Name=viewModel.Name
                    };

                    IdentityResult result = await userManager.CreateAsync(user, viewModel.Password);

                    if (result.Succeeded)
                    {
                        ApplicationUser appUser = await userManager.FindByEmailAsync(user.Email);

                        return RedirectToAction("Index", "Account", new { Areas = "ConstructionAdmin" });
                    }
                    else
                    {
                        AddErrors(result);

                        if (ModelState.ErrorCount != 0)
                        {
                            return View(viewModel);
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Some error occured. Please try again.");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            if (ModelState.IsValid)
            {
                try
                {
                  
                    ApplicationUser appUser = await userManager.FindByEmailAsync(loginModel.Email);
                   
                    if (appUser != null)
                    {
                        //bool checkPassword = await userManager.CheckPasswordAsync(appUser, loginModel.Password);
                         await _signInManager.SignOutAsync();
                         var result=  await _signInManager.PasswordSignInAsync(appUser,loginModel.Password,true,true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Dashboard", new { Areas = "ConstructionAdmin" });
                        }
                        //if (checkPassword)
                        //{
                        //    await _signInManager.SignInAsync(appUser, false);
                        //    return RedirectToAction("Index", "Dashboard", new { Areas = "ConstructionAdmin" });
                        //}
                        else
                        {
                            ModelState.AddModelError("password", "Password is not correct.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("email", "This email does not exist.");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Some error occured. Please try again.");
                }
            }
            return View(loginModel);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {

            ApplicationUser appUser = await userManager.GetUserAsync(User);

            if (appUser == null)
            {
                return Json(new
                {
                    message = ""
                });
            }

            var userRole = await _identityDb.UserRoles.Where(m => m.UserId == appUser.Id).FirstOrDefaultAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new UserEditModel
            {
                Id = appUser.Id,
                Username = appUser.UserName,
                Name = appUser.Name,
                Email = appUser.Email,
            });
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditModel userEditModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser appUser = await userManager.FindByIdAsync(id);

                    if (appUser == null)
                        throw new NullReferenceException();
                    appUser.Email = userEditModel.Email;
                    appUser.Name = userEditModel.Name;
                    appUser.UserName = userEditModel.Username;
                    appUser.PasswordHash = (!String.IsNullOrWhiteSpace(userEditModel.Password)) ? userManager.PasswordHasher.HashPassword(appUser, userEditModel.Password) : appUser.PasswordHash;
                    IdentityResult result = await userManager.UpdateAsync(appUser);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Account", new { Areas = "Constructionadmin" });
                    }
                    else
                    {
                        AddErrors(result);
                        if (ModelState.ErrorCount != 0)
                        {
                            return View(userEditModel);
                        }
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Some error occured. Please try again");
                }
            }
            return View(userEditModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(Login));
            }
            catch { }

            return RedirectToAction("index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Required][FromForm] string id)
        {
            if (ModelState.IsValid)
            {
                IDbContextTransaction transaction = null;
                try
                {
                    var currentUser = await userManager.GetUserAsync(HttpContext.User);

                    if (currentUser.Id == id)
                        throw new Exception("You can not delete own");

                    ApplicationUser user = await userManager.FindByIdAsync(id);

                    if (user == null)
                        throw new NullReferenceException();

                    IdentityResult result = await userManager.DeleteAsync(user);

                    if (!result.Succeeded)
                        throw new Exception();
                    transaction.Commit();
                    return Json(new
                    {
                        status = HttpStatusCode.OK
                    });
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                finally
                {
                    if (transaction != null)
                        transaction.Dispose();
                }
            }

            return Json(new
            {
                status = HttpStatusCode.NotFound
            });
        }
    }
}
