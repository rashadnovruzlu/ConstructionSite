using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConstructionSite.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ServiceViewComponent(IUnitOfWork unitOfWork,
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
            var result =  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.SubServices)
                .Include(x=>x.ServiceImages)
                .Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                    Tittle=x.FindTitle(_lang),
                    //image=x.Image.Path    
                }).ToList();

            if (result.Count == 0 | result == null)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.DataDoesNotExists));
            }
            return View(result);
        }
    }
}