using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.DTO.AdminViewModels.Contact;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        [HttpGet]
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
            var contactResult = _unitOfWork.ContactRepository.GetAll()
                                            .Select(y => new ContactViewModel
                                            {
                                                Tittle = y.FindTitle(_lang),
                                                Content = y.FindContent(_lang),
                                                Address = y.Address,
                                                PhoneNumber = y.PhoneNumber,
                                                Email = y.Email
                                            }).ToList();
            return View(contactResult);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "Bad Request"
                });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    message = "Bad Request"
                });
            }
            if (viewModel == null)
            {
                return Json(new
                {
                    message = "View Model is Empty"
                });
            }
            var contactAddModelResult = new Contact
            {
                Id = viewModel.Id,
                TittleAz = viewModel.TittleAz,
                TittleEn = viewModel.TittleEn,
                TittleRu = viewModel.TittleRu,
                ContentAz = viewModel.ContentAz,
                ContentEn = viewModel.ContentEn,
                ContentRu = viewModel.ContentRu,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email
            };
            var addContactResult = await _unitOfWork.ContactRepository
                                                        .AddAsync(contactAddModelResult);
            if (!addContactResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while creating Contact");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Contact", new { Areas = "ConstructionAdmin" });
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data is not exists");
            }

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "The models are not true"
                });
            }

            var result = _unitOfWork.ContactRepository.GetAll()
                                        .Select(y => new ContactUpdateViewModel
                                        {
                                            Id=y.Id,
                                            TittleAz=y.TittleAz,
                                            TittleEn=y.TittleEn,
                                            TittleRu=y.TittleRu,
                                            ContentAz=y.ContentAz,
                                            ContentEn=y.ContentEn,
                                            ContentRu=y.ContentRu,
                                            Address=y.Address,
                                            PhoneNumber=y.PhoneNumber,
                                            Email=y.Email
                                        }).FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                ModelState.AddModelError("", "Errors occured while editing Contact");
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ContactUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "The models are not true"
                });
            }
            if (viewModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }
            var updateContactModel = new Contact
            {
                Id = viewModel.Id,
                TittleAz = viewModel.TittleAz,
                TittleEn = viewModel.TittleEn,
                TittleRu = viewModel.TittleRu,
                ContentAz = viewModel.ContentAz,
                ContentEn = viewModel.ContentEn,
                ContentRu = viewModel.ContentRu,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email
            };
            var contactResult = await _unitOfWork.ContactRepository
                                                    .UpdateAsync(updateContactModel);
            if (!contactResult.IsDone)
            {
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Contact", new { Areas = "ConstructionAdmin" });
        }

        #endregion


    }
   
}