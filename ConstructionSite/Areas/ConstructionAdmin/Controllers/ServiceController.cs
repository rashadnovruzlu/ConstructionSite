using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.IO;
using System.Linq;
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
            var result=_unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices)
                .Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    NameAz=x.NameAz,
                    NameEn=x.NameEn,
                    NameRu=x.NameRu,
                    TittleAz=x.TittleAz,
                    TittleEn=x.TittleEn,
                    TittleRu=x.TittleRu,
                    Image=x.Image.Path
                })
                .ToList();
            return View(result);
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
                Image image = new Image();
              
                service.ImageId = await FileData.SaveImage(_env, "service", image,_unitOfWork);
              
                if (await _unitOfWork.ServiceRepository.AddAsync(service) > 0)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
