using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ProjectController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(Project project)
        { 
          
          //  _unitOfWork.portfolioRepository.Add()
            return View();
        }
    }
}
