using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.DTO.FrontViewModels.SubService;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public ServicesController(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var allServiceResult = _unitOfWork.ServiceRepository.GetAll()
               .Select(x => new ServiceViewModel
               {
                   Id = x.Id,
                   Name = x.FindName(_lang),
                   Tittle = x.FindName(_lang),
                   image = x.Image.Path
               }).ToList();
            if (allServiceResult == null)
            {
            }
            return View(allServiceResult);
        }

        public IActionResult Services(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
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
                   Name = x.SubService.FindName(_lang),
                   Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()
               }).OrderByDescending(x => x.id)
               .FirstOrDefault();
            if (ServiceSubServiceresult == null)
            {
                return RedirectToAction("Index");
            }
            return View(ServiceSubServiceresult);
        }

        public IActionResult SubService(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var serviceSubServiceresult = _unitOfWork.SubServiceImageRepository.GetAll()
                 .Include(x => x.SubService.Service)

                 .Include(x => x.SubService)
                 .Include(x => x.SubService.SubServiceImages)
                  .Where(x => x.SubServiceId == id)
                 .Select(x => new ServiceSubServiceImage
                 {
                     id = x.Id,

                     SubServiceID = x.SubServiceId,
                     Content = x.SubService.FindContent(_lang),
                     Name = x.SubService.FindName(_lang),
                     Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()
                 }).OrderByDescending(x => x.id)
                 .FirstOrDefault();
            if (serviceSubServiceresult == null)
            {
                ModelState.AddModelError("", "data is null");
            }
            return View(serviceSubServiceresult);
        }

        //public IActionResult Single(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        ModelState.AddModelError("", "Bad Request");
        //    }
        //    var ServiceSubServiceresult = _unitOfWork.SubServiceImageRepository.GetAll()
        //     .Include(x => x.SubService.Service)
        //      .Include(x => x.SubService)
        //     .Include(x => x.SubService.SubServiceImages)
        //     .Where(y => y.SubService.ServiceId == id)
        //     .Select(x => new ServiceSubServiceImage
        //     {
        //         id = x.Id,

        //         SubServiceID = x.SubServiceId,
        //         Content = x.SubService.FindContent(_lang),
        //         Name = x.SubService.FindName(_lang),
        //         Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()

        //     }).OrderByDescending(x => x.id)
        //     .FirstOrDefault();
        //    if (resultOnlySingleServcie==null)
        //    {
        //    }
        //    ViewBag.img=GetImageByServiceID(id);
        //    return View(resultOnlySingleServcie);
        //}

        //private object GetImageByServiceID(int id)
        //{
        //    var serviceSubServiceresult = _unitOfWork.SubServiceImageRepository.GetAll()
        //        .Include(x => x.SubService.Service)

        //        .Include(x => x.SubService)
        //        .Include(x => x.SubService.SubServiceImages)
        //         .Where(x => x.SubServiceId == id)
        //        .Select(x => new ServiceImage
        //        {
        //            Images = x.SubService.SubServiceImages.Select(x =>  x.Image.Path).ToList()

        //        }).ToList();
        //    return serviceSubServiceresult;
        //}
    }
}