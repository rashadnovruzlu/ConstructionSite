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
        string                       _lang;
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
      
       
    }
}