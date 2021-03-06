﻿using ConstructionSite.DTO.FrontViewModels.Contact;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class AdressViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public AdressViewComponent(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.GetLanguages();
            _localizationHandle = localizationHandle;
        }

        public IViewComponentResult Invoke()
        {
            var contactResult = _unitOfWork.ContactRepository.GetAll()
                  .Select(x => new ContactIndexViewModel
                  {
                      Tittle = x.FindTitle(_lang),
                      Content = x.FindContent(_lang),
                      Address = x.Address,
                      Email = x.Email,
                      PhoneNumber = x.PhoneNumber
                  }).FirstOrDefault();

            return View(contactResult);
        }
    }
}