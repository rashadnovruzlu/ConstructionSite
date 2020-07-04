using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
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
        [HttpGet]
        public IActionResult Index()
        {
            var result = _unitOfWork.AboutImageRepository.GetAll()
            .Include(x => x.About)
            .Include(x => x.Image)
            .Select(y => new AboutViewModel
            {
                Id=y.About.Id,
                TittleAz = y.About.TittleAz,
                TittleEn = y.About.TittleEn,
                TittleRu = y.About.TittleRu,
                ContentAz = y.About.ContentAz,
                ContentEn = y.About.ContentEn,
                ContentRu = y.About.ContentRu,
                Image = y.Image.Path
            }).ToList();
           
           return View(result);

          
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
        public async Task<IActionResult> Delete(int id)
        {
            var data=await _unitOfWork.AboutImageRepository.GetByIdAsync(id);
            var result=await _unitOfWork.AboutImageRepository.DeleteAsync(data);
            return RedirectToAction("Index");
        }
    }
}
