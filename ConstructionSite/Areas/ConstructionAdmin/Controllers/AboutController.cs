using AutoMapper;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{


    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public AboutController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _unitOfWork = unitOfWork;
            _env=env;
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
        public async Task<IActionResult> Add(About about,IFormFile FileData)
        {
            

            if (ModelState.IsValid)
            {
                AboutImage aboutImage = new AboutImage();
               
                Image image = new Image();
                aboutImage.ImageId = await FileData.SaveImage(_env,"about",image,_unitOfWork);
                    
              
                aboutImage.AboutId =await _unitOfWork.AboutRepository.AddAsync(about);
                if(await _unitOfWork.AboutImageRepository.AddAsync(aboutImage)>0)
                    return RedirectToAction("Index");
            }
           
           
           
            return View();
        }
    }
}
