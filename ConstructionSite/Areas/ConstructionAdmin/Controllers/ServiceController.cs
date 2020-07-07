using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{

    [Area(nameof(ConstructionAdmin))]
    public class ServiceController : Controller
    {



        private string                        _lang;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment  _env;
        public ServiceController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env, 
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _lang = _httpContextAccessor.getLang();

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
            var result = _unitOfWork.ServiceRepository.GetAll()
                .Include(x => x.Image)
                .Include(x => x.SubServices)
                .Select(x => new ServiceViewModel
                {
                    Id = x.Id,
                    Name = x.FindName(_lang),
                    Tittle = x.FindTitle(_lang),
                    Image = x.Image.Path
                })
                .ToList();
            return View(result);
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Service service, IFormFile FileData)
        {
            if (service==null)
            {
                return RedirectToAction("Index");
            }
            int imageresultID=0;
            Image image = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            if (FileData is null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new
                {
                    message = "file not found BadRequest"
                });
            }
            imageresultID = await FileData.SaveImage(_env, "service", image, _unitOfWork);
            if (imageresultID < 0)
            {
                Response.StatusCode = (int)HttpStatusCode.SeeOther;

                return Json(new
                {
                    message = "file not save"
                });
            }
            service.ImageId=imageresultID;
            var serviceResult = await _unitOfWork.ServiceRepository.AddAsync(service);
            if (serviceResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            else
            {
                FileData.Delete(_env,image,"service");
                _unitOfWork.Rollback();

            }
            _unitOfWork.Dispose();
            return View();
        }
    }
}

