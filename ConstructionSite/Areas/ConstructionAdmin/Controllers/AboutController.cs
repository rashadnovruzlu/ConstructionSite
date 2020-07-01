using AutoMapper;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{


    [Area(nameof(ConstructionAdmin))]
  
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAboutRepository _aboutRepository;
        public AboutController(IUnitOfWork unitOfWork, 
                               IMapper mapper,
                               IAboutRepository aboutRepository)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _aboutRepository=aboutRepository;
            


        }
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(About about)
        {
            return View();
        }
    }
}
