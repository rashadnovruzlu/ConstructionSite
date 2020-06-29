using AutoMapper;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{


    [Area(nameof(ConstructionAdmin))]
  
    public class AboutController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAboutRepository aboutRepository;
        public AboutController(IUnitOfWork unitOfWork, 
                               IMapper mapper,
                               IAboutRepository aboutRepository)
        {
            this.unitOfWork=unitOfWork;
            this.mapper = mapper;
            this.aboutRepository=aboutRepository;
            

        }
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
