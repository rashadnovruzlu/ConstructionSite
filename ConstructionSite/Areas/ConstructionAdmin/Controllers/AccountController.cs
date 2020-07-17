using ConstructionSite.DTO.AdminViewModels.Account;
using ConstructionSite.Entity.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.Admin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
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
        //[Route("Create")]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       // [Route("Create")]
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
                try
                {
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(loginModel.Email);

                    if (appUser != null)
                    {
                        await _signInManager.SignOutAsync();
                        var result = await _signInManager.PasswordSignInAsync(appUser, loginModel.Password, true, true);
                        if (result.Succeeded)
                        {
                            return Redirect(returnUrl ?? "/");
                        }
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

        #endregion

        #region EDIT

        [HttpGet]
      //  [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
        
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
        //[Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditModel userEditModel)
        {
           
            if (!ModelState.IsValid)
            {
                
            }
            var user=await    _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                if (!string.IsNullOrEmpty(userEditModel.Password))
                {
              var   validpass=  await  _passwordValidator.ValidateAsync(_userManager,user,userEditModel.Password);
                    if (validpass.Succeeded)
                    {
                      user.PasswordHash=  _passwordHasher.HashPassword(user,userEditModel.Password);
                    }
                    else
                    {

                        foreach (var item in validpass.Errors)
                        {
                            ModelState.AddModelError("",item.Description.ToString());
                        }
                    }
                   
                }
               
               

            }
           ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = userEditModel.Email,
                    UserName = userEditModel.Username
                };
           user.Email=userEditModel.Email;
            user.Name=userEditModel.Name;
            user.UserName=userEditModel.Username;
          var result=await  _userManager.UpdateAsync(user);
            foreach (var item in result.Errors)
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
                return RedirectToAction("index", "Dashboard");
            
          
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

                    return View("Index", _userManager.Users);
                }
            }
           
            return View("Index",_userManager.Users);

        }

        #endregion
    }
}