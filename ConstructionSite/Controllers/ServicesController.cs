using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private string _lang;
        //public ServicesController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        //    var culture = rqf.RequestCulture.Culture;
        //    _lang = culture.Name;


        //}
        //public IActionResult Index()
        //{
           
        //  var result=  _unitOfWork.ServiceRepository.GetAll()
        //        .Include(x=>x.Image)
        //        .Include(x=>x.SubServices)
        //        .Select(x=>new ServiceViewModel
        //        {
        //            Id=x.Id,
        //            Name=x.FindName(_lang),
        //            Tittle=x.FindTitle(_lang),
        //            image=x.Image.Path,
        //           // SubServices=x.SubServices

                    
        //        })
                
        //        .ToList();
        //    return View(result);
        //}
        //public IActionResult Single(int id)
        //{
        //  var result=  _unitOfWork.ServiceRepository.GetAll()
        //        .Include(x=>x.Image)
        //        .Include(x=>x.SubServices)
        //        .Select(x=>new SingleServiceViewModel
        //        {
        //            Id=x.Id,
                  
        //            Name=x.FindName(_lang),
        //            Tittle=x.FindTitle(_lang),
                   
        //            image=x.Image.Path


        //        }).FirstOrDefault(x=>x.Id==id);
        //    return View(result);
        //}
        public IActionResult Construction()
        {
            return View();
        }
        
        public IActionResult Renovation()
        {
            return View();
        }

        public IActionResult Consulting()
        {
            return View();
        }

        public IActionResult Architecture()
        {
            return View();
        }

        public IActionResult Electrical()
        {
            return View();
        }
    }
}