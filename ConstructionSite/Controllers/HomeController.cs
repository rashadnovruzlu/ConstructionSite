using ConstructionSite.DTO.FrontViewModels.About;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ConstructionSite.Controllers
{
    public class HomeController : Controller
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        

        public HomeController(IUnitOfWork unitOfWork
                                 )
        {
            _unitOfWork = unitOfWork;
         
            //_lang = Response..getLang();

        }
        public IActionResult Index()
        {
            return View();
        }
      
        //public IActionResult Blog()
        //{
        //    var aboutImageResult = _unitOfWork.AboutImageRepository.GetAll()
        //       .Select(x => new AboutViewModel
        //       {
        //           Id = x.Id,
        //           AboutID = x.AboutId,
        //           Content = x.About.FindContent(_lang),
        //           Tittle = x.About.FindTitle(_lang),
        //           Image = x.Image.Path,
        //           imageId = x.ImageId
        //       }).ToList()
        //       .FirstOrDefault();
        //    return View();
        //}
    }
}