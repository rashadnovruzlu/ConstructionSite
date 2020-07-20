using ConstructionSite.DTO.AdminViewModels.Service;
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
        public IActionResult Single(int id)
        {
            var resultOnlySingleServcie=_unitOfWork.ServiceRepository.GetAll()
                .Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                    Tittle=x.FindTitle(_lang),
                    Image=x.Image.Path
                }).FirstOrDefault(x=>x.Id==id);
            if (resultOnlySingleServcie==null)
            {

            }
            return View(resultOnlySingleServcie);
        }
        public IActionResult Inner(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                return RedirectToAction("Index");
            }


            var ServiceSubServiceresult = _unitOfWork.SubServiceImageRepository.GetAll()
               .Include(x => x.SubService.Service)

               .Include(x => x.SubService)
               .Include(x => x.SubService.SubServiceImages)
               .Where(y => y.SubService.ServiceId == id)
               .Select(x => new ServiceSubServiceImage
               {
                   id = x.Id,

                   SubServiceID = x.SubServiceId,
                   Content = x.SubService.FindContent(_lang),
                   SubName = x.SubService.FindName(_lang),
                   Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()

               }).OrderByDescending(x => x.id)
               .FirstOrDefault();


            return View(ServiceSubServiceresult);

        }
        public IActionResult subservice(int id)
        {
            return View();
        }

    }
}