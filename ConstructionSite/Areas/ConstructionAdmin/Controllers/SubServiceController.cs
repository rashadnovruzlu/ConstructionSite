using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class SubServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public SubServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork=unitOfWork;
            _env=env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            string str1=_env.WebRootPath;
            string str2=_env.ContentRootPath;
            ViewBag.data=_unitOfWork.ServiceRepository.GetAll().ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SubService subService,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file!=null)
                {
                    Image image=new Image();
                    SubServiceImage sub=new SubServiceImage();
                    sub.ImageId=await file.SaveImage(_env,"subserver",image,_unitOfWork);
                    sub.SubServiceId= await _unitOfWork.SubServiceRepository.AddAsync(subService);
                    
                    if( await _unitOfWork.SubServiceImageRepository.AddAsync(sub) > 0)
                        return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.data = _unitOfWork.ServiceRepository.GetAll().ToList();
                }
            }
            return View(subService);
        }
    }
}
