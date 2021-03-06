﻿using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Services;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceQueryFacade _serviceQueryFacade;
        private readonly SharedLocalizationService _localizationHandle;

        public ServiceViewComponent(IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor,
                                    SharedLocalizationService localizationHandle,
                                    IServiceQueryFacade serviceQueryFacade)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.GetLanguages();
            _localizationHandle = localizationHandle;
            _serviceQueryFacade = serviceQueryFacade;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!ModelState.IsValid)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var result = await _serviceQueryFacade.GetAll(_lang);

            return await Task.FromResult<IViewComponentResult>(View(result.AsEnumerable()));
        }
    }
}