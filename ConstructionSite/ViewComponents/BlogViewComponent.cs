using ConstructionSite.Helpers.Constants;
using ConstructionSite.Helpers.Interfaces;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        string _lang;
        private readonly UnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISharedLocalizationHandle _localizationHandle;

        public BlogViewComponent(UnitOfWork unitOfWork,
                                 IHttpContextAccessor contextAccessor,
                                 ISharedLocalizationHandle localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _localizationHandle = localizationHandle;
            _lang = _contextAccessor.getLang();
        }

        public IViewComponentResult Invoke()
        {
            if (!ModelState.IsValid)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", _localizationHandle.GetLocalizationByKey(RESOURCEKEYS.BadRequest));
            }

            var newsImageResult= _unitOfWork.newsImageRepository.GetAll()
                                                .Select(x=>new BlogViewModel)

            return View();
        }
    }
}
