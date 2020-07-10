using ConstructionSite.DTO.AdminViewModels.Description;
using ConstructionSite.DTO.AdminViewModels.SubService;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class SubServiceController : Controller
    {
        private string                        _lang;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IWebHostEnvironment  _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _env = env;
            _lang=_httpContextAccessor.getLang();
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }

            var SubServiceImage = _unitOfWork.SubServiceImageRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubService)
                .Select(x=>new SubServiceViewModel
                {
                    ImagePath=x.Image.Path,
                    Name=x.SubService.FindName(_lang),
                    Content=x.SubService.FindContent(_lang)
                })
                .ToList();
            if (SubServiceImage==null&& SubServiceImage.Count<1)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this is empty");
            }
           
            _unitOfWork.Dispose();
            return View(SubServiceImage);
        }
        [HttpGet]
       
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Select(x=>new ServiceSubServiceAddView
                {
                    Id=x.Id,
                    Name=x.FindName(_lang)
                }).ToList();
            if (result.Count<0)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this is empty");
               
            }
            _unitOfWork.Dispose();
            ViewBag.data = result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SubService subService, IFormFile file)
        {
            int imageresultID=0;
            SubServiceImage sub = new SubServiceImage();
            Image image = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (file is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new
                {
                    message = "file not found BadRequest"
                });
            }

            imageresultID = await file.SaveImage(_env, "subserver", image, _unitOfWork);
            if (imageresultID < 0)
            {
                return Json(new
                {
                    message = "file not save"
                });
            }
            sub.ImageId=imageresultID;
            var SubServiceResult = await _unitOfWork.SubServiceRepository.AddAsync(subService);
            if (SubServiceResult.IsDone)
            {
                sub.SubServiceId = subService.Id;
            }
            var SubServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(sub);
            if (!SubServiceImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "added error");
            }
            _unitOfWork.Dispose();
            ViewBag.data = _unitOfWork.ServiceRepository.GetAll().ToList();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (id<1)
            {
                ModelState.AddModelError("","this is empty");
            }
            var subServiceImageResult=await _unitOfWork.SubServiceImageRepository.GetByIdAsync(id);
            if (subServiceImageResult==null)
            {
                ModelState.AddModelError("", "this is empty");
            }
            
            return View(subServiceImageResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id)
        {
            return View();
        }
    }
}