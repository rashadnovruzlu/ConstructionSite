using ConstructionSite.DTO.AdminViewModels.Account;
using ConstructionSite.Entity.Identity;
using ConstructionSite.Helpers.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class AccountController : Controller
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
       
      //  private readonly ApplicationIdentityDbContext _identityDb;
        #endregion

        #region AddErrors Method
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        #endregion

        #region CTOR
        public AccountController(ApplicationIdentityDbContext identityDb, 
                                 UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IPasswordValidator<ApplicationUser> passwordValidator,
                                 IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            _passwordValidator=passwordValidator;
            _passwordHasher=passwordHasher;
        }
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region CREATE

        [HttpGet]
        
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

            }
            
            ApplicationUser user=new ApplicationUser();
            user.UserName=viewModel.Username;
            user.Email=viewModel.Email;
          var result=  await  _userManager.CreateAsync(user,viewModel.Password);
            if (result.Succeeded)
            {

                return RedirectToAction("Index");
            }
            else
            {

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description.ToString());
                }
            }

            return View();
        }

        #endregion

        #region LOGIN

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl= returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByEmailAsync(loginModel.Email);

                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    var result=  await  _signInManager.PasswordSignInAsync(appUser,loginModel.Password,true,true);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "This email does not exist.");
                }
            }
            ViewBag.returnUrl= returnUrl;
            return View();
        }

        #endregion

        #region EDIT

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
              return RedirectToAction("Index");
            }
            var userResult=await _userManager.FindByIdAsync(id);
            if (userResult!=null)
            {
                UserEditModel userEditModel=new UserEditModel
                {
                    Username=userResult.UserName,
                    Email=userResult.Email,
                    Id=userResult.Id,
                    Name=userResult.Name,
                    Password=userResult.PasswordHash,
                };
                return View(userEditModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditModel userEditModel)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var userDataResult=await    _userManager.FindByIdAsync(id);
            if (userDataResult!=null)
            {
                if (!string.IsNullOrEmpty(userEditModel.Password))
                {
              var   valideterpasswor=  await  _passwordValidator.ValidateAsync(_userManager,userDataResult,userEditModel.Password);
                    if (valideterpasswor.Succeeded)
                    {
                      userDataResult.PasswordHash=  _passwordHasher.HashPassword(userDataResult,userEditModel.Password);
                    }
                    else
                    {
                        foreach (var item in valideterpasswor.Errors)
                        {
                            ModelState.AddModelError("",item.Description.ToString());
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(userEditModel.Email))
            {
                userDataResult.Email = userEditModel.Email;
            }
            if (!string.IsNullOrEmpty(userEditModel.Name))
            {
                userDataResult.Name = userEditModel.Name;
            }
            if (!string.IsNullOrEmpty(userEditModel.Username))
            {
                userDataResult.UserName = userEditModel.Username;
            }
            var userUpdateresult=await  _userManager.UpdateAsync(userDataResult);
            if (userUpdateresult.Succeeded)
            {
               return RedirectToAction("Index");
            }
            foreach (var item in userUpdateresult.Errors)
            {
                ModelState.AddModelError("",item.Description.ToString());
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region LOGOUT

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }
      
        #endregion

        #region DELETE

      //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var userResult=await _userManager.FindByIdAsync(id);
            if (userResult!=null)
            {
                var identityResult=   await   _userManager.DeleteAsync(userResult);
                if (!identityResult.Succeeded)
                {
                    foreach (var item in identityResult.Errors)
                    {
                        ModelState.AddModelError("",item.Description.ToString());
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion
     
    }
}