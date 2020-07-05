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
            //_unitOfWork.ServiceRepository.GetAll()
            //    .Include(x=>x)
          var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices)
                .Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    Name=x.NameAz,
                    Tittle=x.fi,
                    image=x.Image.Path,
                    SubServices=x.SubServices

                    
                })
                
                .ToList();
            return View(result);
        }
        public IActionResult Single(int id)
        {
          var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices)
                .Select(x=>new SingleServiceViewModel
                {
                    Id=x.Id,
                    NameAz=x.NameAz,
                    NameEn=x.NameEn,
                    NameRu=x.NameRu,
                    TittleAz=x.TittleAz,
                    TittleEn=x.TittleEn,
                    image=x.Image.Path


                }).FirstOrDefault(x=>x.Id==id);
            return View(result);
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