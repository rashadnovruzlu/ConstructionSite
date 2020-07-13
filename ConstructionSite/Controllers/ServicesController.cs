using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ServicesController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }
        public IActionResult Index(int id)
        {
            var resut=_unitOfWork.SubServiceImageRepository.GetById(id);
               
             
              
            return View();
        }


    }
}