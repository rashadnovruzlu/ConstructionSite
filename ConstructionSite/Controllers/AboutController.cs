using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Data;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public AboutController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async IActionResult Index()

        { 
          
           
           
               
               
          
            return View();
        }
    }
}