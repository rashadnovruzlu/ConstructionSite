//using ConstructionSite.Areas.Admin.Models;
using ConstructionSite.Entity.Identity;
using ConstructionSite.Helpers.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _UserManager;
        public AdminController(UserManager<ApplicationUser> UserManager)
        {
            _UserManager=UserManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_UserManager.Users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> Create(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user=new  ApplicationUser();
        //        user.UserName=model.Name;
        //        user.Email=model.Email;
        //        var result=await _UserManager.CreateAsync(user,model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            foreach (var item in result.Errors)
        //            {
        //                ModelState.AddModelError("",item.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
        var user=  await  _UserManager.FindByIdAsync(id);
            if (user==null)
            {
                ModelState.AddModelError("", "User Not Exists");
                throw new UserNotExistsException("User Not Exists");

            }
            if (user!=null)
            {
              var result=await  _UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View("Index",_UserManager.Users);
        }
        public async Task<IActionResult> Update(string id)
        {
            var user=await _UserManager.FindByIdAsync(id);
            if (user==null)
            {
                ModelState.AddModelError("", "User Not Exists");
                throw new UserNotExistsException("User Not Exists");
            }
            if (user!=null)
            {
                return View(user);
            }
            else
            {

                return RedirectToAction("Index");
            }
           
        }
    }
}