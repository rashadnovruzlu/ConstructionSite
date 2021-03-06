﻿using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ConstructionSite.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public AboutViewComponent(IUnitOfWork unitOfWork,
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
            if (!ModelState.IsValid)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            var aboutImageResult = _unitOfWork.AboutImageRepository.GetAll()
                .Select(x => new AboutViewModel
                {
                    Id = x.Id,
                    AboutID = x.AboutId,
                    Content = x.About.FindContent(_lang),
                    Tittle = x.About.FindTitle(_lang),
                    Image = x.Image.Path,
                    imageId = x.ImageId
                }).ToList()
                .FirstOrDefault();
            if (aboutImageResult == null)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
            }
            ViewBag.with = 500;
            ViewBag.hid = 500;
            return View(aboutImageResult);
        }
    }
}