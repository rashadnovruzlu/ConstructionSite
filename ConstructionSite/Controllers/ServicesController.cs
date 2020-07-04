using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.DTO.FrontViewModels;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public ServicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            

        }
        public IActionResult Index()
        {
          var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices)
                .Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    NameAz=x.NameAz,
                    NameRu=x.NameRu,
                    NameEn=x.NameEn,
                    TittleAz=x.TittleAz,
                    TittleEn=x.TittleEn,
                    TittleRu=x.TittleRu,
                    image=x.Image.Path,
                    SubServices=x.SubServices

                    
                })
                .ToList();
            return View(result);
        }
        public IActionResult Single()
        {
            return View();
        }
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