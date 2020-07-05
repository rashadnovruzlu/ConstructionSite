using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
        private string _lang;
        public AboutController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _unitOfWork = unitOfWork;
            _env=env;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            _lang = culture.Name;
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
                Tittle=y.About.FindTitle(_lang),
                Content=y.About.FindContent(_lang),
               
                Image = y.Image.Path
            }).ToList();
           
           return View(result);

          
        }
        #region --Add--
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(About about, IFormFile FileData)
        {


            if (ModelState.IsValid)
            {
                AboutImage aboutImage = new AboutImage();

                Image image = new Image();
                aboutImage.ImageId = await FileData.SaveImage(_env, "about", image, _unitOfWork);


                aboutImage.AboutId = await _unitOfWork.AboutRepository.AddAsync(about);
                if (await _unitOfWork.AboutImageRepository.AddAsync(aboutImage) > 0)
                    return RedirectToAction("Index");
            }



            return View();
        }
        #endregion
        #region --Update--
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Update(AboutImage about)
        {
            return View();
        }
        #endregion
        public async Task<IActionResult> Delete(int id)
        {

            var data=       await _unitOfWork.AboutImageRepository.GetByIdAsync(id);
            var about=      await _unitOfWork.AboutRepository.GetByIdAsync(data.AboutId);
            var image=      await _unitOfWork.imageRepository.GetByIdAsync(data.ImageId);
            var aboutResult=await _unitOfWork.AboutRepository.DeleteAsync(about);
            var imageResult=await _unitOfWork.imageRepository.DeleteAsync(image);
            var result=     await _unitOfWork.AboutImageRepository.DeleteAsync(data);
            if (aboutResult.IsDone==true&&imageResult.IsDone==true&&result.IsDone==true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","an error whene delete data");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }
    }
}
