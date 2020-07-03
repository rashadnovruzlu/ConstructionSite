using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;

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
        public async Task<IActionResult> Add(Service service, IFormFile FileData)
        {
            if (ModelState.IsValid)
            {
                if (FileData.IsImage())
                {
                    Image image = new Image();
                    string name = await FileData.SaveAsync(_env, "service");
                    image.Title = name;
                    image.Path = Path.Combine(_env.WebRootPath, "images", name);
                    service.ImageId = await _unitOfWork.imageRepository.AddAsync(image);
                }
                if (await _unitOfWork.ServiceRepository.AddAsync(service) > 0)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
