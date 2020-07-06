using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class SubServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public SubServiceController(IUnitOfWork unitOfWork,
                                    IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

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
            return View();
        }

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
            ViewBag.data = _unitOfWork.ServiceRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubService subService, IFormFile file)
        {
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

            sub.ImageId = await file.SaveImage(_env, "subserver", image, _unitOfWork);
            if (sub.ImageId < 0)
            {
                return Json(new
                {
                    message = "file not save"
                });
            }
            var SubServiceResult = await _unitOfWork.SubServiceRepository.AddAsync(subService);
            if (SubServiceResult.IsDone)
            {
                sub.SubServiceId = subService.Id;
            }
            var SubServiceImageResult = await _unitOfWork.SubServiceImageRepository.AddAsync(sub);
            if (!SubServiceImageResult.IsDone)
            {
                return RedirectToAction("Index");
            }
            ViewBag.data = _unitOfWork.ServiceRepository.GetAll().ToList();
            return View();
        }
    }
}