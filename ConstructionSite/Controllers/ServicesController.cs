using ConstructionSite.DTO.FrontViewModels.SubService;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
       
        public IActionResult Inner(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id<1)
            {
                return RedirectToAction("Index");
            }


            var result = _unitOfWork.SubServiceImageRepository.GetAll()
               .Include(x => x.SubService.Service)
               .Include(x => x.Image)
               .Include(x => x.SubService)
               .Where(y => y.SubService.ServiceId == id)
               .Select(x => new ServiceSubServiceImage
               {
                   id = x.Id,
                   SubServiceID = x.SubServiceId,
                   Content = x.SubService.FindContent(_lang),
                   SubName = x.SubService.FindName(_lang)
               }).FirstOrDefault();
        

            return View();

        }
        public IActionResult subservice(int id)
        {
            return View();
        }

    }
}