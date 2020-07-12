using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class MessageViewComponent:ViewComponent
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public MessageViewComponent(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
        }
}
