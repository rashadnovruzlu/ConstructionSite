using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class ContactController : Controller
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ContactController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Message message)
        {

            return View();
        }
    }
}
