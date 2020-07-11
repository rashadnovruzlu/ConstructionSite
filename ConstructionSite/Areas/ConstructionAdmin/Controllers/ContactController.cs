using ConstructionSite.DTO.AdminViewModels.Contact;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class ContactController : Controller
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public ContactController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment environment,
                                 IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _contextAccessor = contextAccessor;
            _lang = _contextAccessor.getLang();
        }
        #region Index
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "Bad Request"
                });
            }
            var contactResult=_unitOfWork.ContactRepository.GetAll()
                                            .Select(y=>new ContactViewModel
                                            {
                                            })

            return View();
        }

        #endregion


        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Add(string n)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Update(string s)
        {
            return View();
        }
    }
   
}