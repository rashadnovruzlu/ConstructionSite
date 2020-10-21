using ConstructionSite.DTO.AdminViewModels.Role;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRoleController(RoleManager<IdentityRole> roleManager,
                                   UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Name)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (string.IsNullOrEmpty(Name))
            {
                ModelState.AddModelError("", "data is null");
                return RedirectToAction("Index");
            }
            var identityRoleResult = await _roleManager.CreateAsync(new IdentityRole(Name));
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

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var memmbers = new List<ApplicationUser>();
            var nomemmbers = new List<ApplicationUser>();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            var identityRoleResult = await _roleManager.FindByIdAsync(id);
            if (identityRoleResult == null)
            {
                ModelState.AddModelError("", "role not exists");
            }
            foreach (var User in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(User, identityRoleResult.Name) ? memmbers : nomemmbers;
                list.Add(User);
            }
            var modelResult = new RoleDetailsViewModel
            {
                Role = identityRoleResult,
                Members = memmbers,
                NoMembers = nomemmbers
            };
            return View(modelResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleEditViewModel roleEditViewModel)
        {
            if (roleEditViewModel != null)
            {
                foreach (var item in roleEditViewModel.IDsToAdd ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(item);
                    if (user != null)
                    {
                        var userRoleResult = await _userManager.AddToRoleAsync(user, roleEditViewModel.RoleName);
                        if (userRoleResult.Succeeded)
                        {
                            foreach (var erro in userRoleResult.Errors)
                            {
                                ModelState.AddModelError("", erro.Description.ToString());
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                foreach (var item in roleEditViewModel.IDsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(item);
                    if (user != null)
                    {
                        var userRoleResult = await _userManager.RemoveFromRoleAsync(user, roleEditViewModel.RoleName);
                        if (userRoleResult.Succeeded)
                        {
                            foreach (var erro in userRoleResult.Errors)
                            {
                                ModelState.AddModelError("", erro.Description.ToString());
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View(roleEditViewModel.RoleID);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var deleteRoleResult = await _roleManager.DeleteAsync(role);
                if (deleteRoleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in deleteRoleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description.ToString());
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}