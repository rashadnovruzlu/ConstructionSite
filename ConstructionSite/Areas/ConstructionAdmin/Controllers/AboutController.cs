using AutoMapper;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{


    [Area(nameof(ConstructionAdmin))]
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
            int imageID=0;

            if (ModelState.IsValid)
            {
                AboutImage aboutImage = new AboutImage();
                Image image = new Image();
                if (FileData.IsImage())
                {
                    string name = await FileData.SaveAsync(_env, "about");
                    image.Title = name;
                    image.Path = Path.Combine(_env.WebRootPath, "images", name);
                    imageID = await _unitOfWork.imageRepository.AddAsync(image);
                }

                aboutImage.AboutId = _unitOfWork.AboutRepository.Add(about);
                aboutImage.ImageId = imageID;
                await _unitOfWork.AboutImageRepository.AddAsync(aboutImage);
            }
           
           
           
            return View();
        }
    }
}
