using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.DTO.FrontViewModels.Contact;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class ContactController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        SharedLocalizationService _localizationHandle;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        public ContactController(IUnitOfWork unitOfWork,
                                 IHttpContextAccessor httpContextAccessor,
                                 SharedLocalizationService localizationHandle)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
        }

        public IActionResult Index()
        {
            var contactData = _unitOfWork.ContactRepository.GetAll()
                                            .Select(x => new ContactIndexViewModel
                                            {
                                                Id=x.Id,
                                                Tittle = x.FindTitle(_lang),
                                                Content = x.FindContent(_lang),
                                                Address = x.Address,
                                                PhoneNumber = x.PhoneNumber,
                                                Email = x.Email
                                            }).ToList()
                                                .OrderByDescending(y => y.Id)
                                                    .FirstOrDefault();
            return View(contactData);
        }
    }
}
