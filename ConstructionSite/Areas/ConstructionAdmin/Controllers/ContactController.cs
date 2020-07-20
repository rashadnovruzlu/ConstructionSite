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
        #region Fields
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        #region CTOR
        public ContactController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment environment,
                                 IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _contextAccessor = contextAccessor;
            _lang = _contextAccessor.getLang();
        }
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
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

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ContactAddViewModel contactAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (contactAddViewModel == null)
            {
                ModelState.AddModelError("", "View Model is empty.");
            }
            var contactAddModelResult = new Contact
            {
                TittleAz = contactAddViewModel.TittleAz,
                TittleEn = contactAddViewModel.TittleEn,
                TittleRu = contactAddViewModel.TittleRu,
                ContentAz = contactAddViewModel.ContentAz,
                ContentEn = contactAddViewModel.ContentEn,
                ContentRu = contactAddViewModel.ContentRu,
                Address = contactAddViewModel.Address,
                PhoneNumber = contactAddViewModel.PhoneNumber,
                Email = contactAddViewModel.Email
            };
            var addContactResult = await _unitOfWork.ContactRepository
                                                        .AddAsync(contactAddModelResult);
            if (!addContactResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while creating Contact");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data is not exists");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }

            var contantcUpdateResult = _unitOfWork.ContactRepository.GetById(id);
            if (contantcUpdateResult == null)
            {
                ModelState.AddModelError("", "Errors occured while editing Contact");
            }
            var updateContactUpdate = new ContactUpdateViewModel
            {
                Id = contantcUpdateResult.Id,
                ContentAz = contantcUpdateResult.ContentAz,
                ContentEn = contantcUpdateResult.ContentEn,
                ContentRu = contantcUpdateResult.ContentRu,
                TittleAz = contantcUpdateResult.TittleAz,
                TittleEn = contantcUpdateResult.TittleEn,
                TittleRu = contantcUpdateResult.TittleRu,
                Address = contantcUpdateResult.Address,
                Email = contantcUpdateResult.Email,
                PhoneNumber = contantcUpdateResult.PhoneNumber
            };

            if (updateContactUpdate == null)
            {
                ModelState.AddModelError("", "Errors occured while editing Contact");
            }
            return View(contantcUpdateResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ContactUpdateViewModel contactUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (contactUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }
            var updateContactModel = new Contact
            {
                Id = contactUpdateViewModel.Id,
                TittleAz = contactUpdateViewModel.TittleAz,
                TittleEn = contactUpdateViewModel.TittleEn,
                TittleRu = contactUpdateViewModel.TittleRu,
                ContentAz = contactUpdateViewModel.ContentAz,
                ContentEn = contactUpdateViewModel.ContentEn,
                ContentRu = contactUpdateViewModel.ContentRu,
                Address = contactUpdateViewModel.Address,
                PhoneNumber = contactUpdateViewModel.PhoneNumber,
                Email = contactUpdateViewModel.Email
            };
            var contactResult = await _unitOfWork.ContactRepository
                                                    .UpdateAsync(updateContactModel);
            if (!contactResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while updating Contact");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            var contactViewModel = await _unitOfWork.ContactRepository.GetByIdAsync(id);
            if (contactViewModel == null)
            {
                ModelState.AddModelError("", "Data is Null ");
            }
            var contatDeleteResult = await _unitOfWork.ContactRepository.DeleteAsync(contactViewModel);
            if (!contatDeleteResult.IsDone)
            {
                ModelState.AddModelError("", "data can,t be delete ");
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion
    }
}