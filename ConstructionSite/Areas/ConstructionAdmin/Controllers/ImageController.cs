using ConstructionSite.DTO.AdminViewModels.Image;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class ImageController : Controller
    {
        private string _lang;
        private readonly  IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        SharedLocalizationService _localizationHandle;
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork,
                               SharedLocalizationService localizationHandle,
                               IHttpContextAccessor httpContextAccessor,
                               IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
            env=_env;

            //_localizationHandle.GetLocalizationByKey(RESOURCEKEYS.)
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            var result=_unitOfWork.AboutRepository.GetAll().Select(x=>new AddAboutViewModel
            {
                Id=x.Id,
                Title=x.FindTitle(_lang)
            }).ToList();
            if (result==null)
            {
                ModelState.AddModelError("","this is errors");
                return RedirectToAction("Add", "About");
            }
           _unitOfWork.Dispose();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> About(int id,IFormFile file)
        {
           
            var aboutImageResult=await _unitOfWork.AboutRepository.GetByIdAsync(id);
            Image image = new Image();
            var result =await file.SaveImage(_env, "about", image, _unitOfWork);
            if (!result)
            {
                ModelState.AddModelError("","this is errors");
            }

          
            await _unitOfWork.AboutRepository.UpdateAsync(aboutImageResult);
           
            _unitOfWork.Dispose();
          
            return RedirectToAction("Index");
        }
        public IActionResult SubService()
        {
            var imageSubServiceResult=_unitOfWork.SubServiceRepository.GetAll()
                .Select(x=>new AddSubServiceViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang)
                }).ToList();
            if (imageSubServiceResult!=null)
            {
                ModelState.AddModelError("", "this is errors");
                return RedirectToAction("Add", "SubService");
            }
            _unitOfWork.Dispose();
            return View(imageSubServiceResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubService(int id,IFormFile file)
        {
            Image image=new Image();
          var subserviceImage=await _unitOfWork.SubServiceRepository.GetByIdAsync(id);
            if (subserviceImage==null)
            {

            }
            await file.SaveImage(_env,"subservice",image,_unitOfWork);
           
            _unitOfWork.SubServiceRepository.Update(subserviceImage);
            _unitOfWork.Dispose();
            return View();
        }
    }
}