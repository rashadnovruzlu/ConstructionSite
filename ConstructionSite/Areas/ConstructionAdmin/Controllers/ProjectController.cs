using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private string                        _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IWebHostEnvironment  _env;

        public ProjectController(IUnitOfWork unitOfWork,
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
                var result= _unitOfWork.projectImageRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.Project)
                .Select(x=>new ProjectViewModel
                {
                    Id=x.Id,
                    Name=x.Project.FindName(_lang),
                    Content=x.Project.FindContent(_lang),
                    Image=x.Image.Path,
                    ImageId=x.ImageId
                }).ToList();
            if (result == null| result.Count==0)
            {
               
                ModelState.AddModelError("", "this list emity");
                return Json(new
                {
                    message = "this list emity"
                });

            }
            else
            {
                _unitOfWork.Dispose();
                return View(result);
            }
          
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
            var portfolioResult=  _unitOfWork.portfolioRepository.GetAll()
                .Select(x=>new PortfolioViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang)
                }).ToList();
            if (portfolioResult !=null)
            {
                ViewBag.items = portfolioResult;
               
            }
            else
            {
                ModelState.AddModelError("", "this list emity");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectAddModel project,IFormFile file)
        {
            Image img = new Image();
            int imageID=0;
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
                return Json(new
                {
                    message = "file not exists"
                });
            }
           
            var result = new Project
            {
                NameAz = project.NameAz,
                NameRu = project.NameRu,
                NameEn = project.NameEn,
                ContentAz = project.ContentAz,
                ContentRu = project.ContentRu,
                ContentEn = project.ContentEn,
                PortfolioId = project.PortfolioId,

            };
            var projectResult=  await _unitOfWork.projectRepository.AddAsync(result);
            if (projectResult.IsDone)
            {
                imageID = await file.SaveImage(_env, "project", img, _unitOfWork);
                if (imageID < 0)
                {
                    return Json(new
                    {
                        message = "file not save"
                    });
                }
                ProjectImage image = new ProjectImage
                {
                    ImageId = imageID,
                    ProjectId = result.Id
                };
                var imageResult= await _unitOfWork.projectImageRepository.AddAsync(image);
                if (imageResult.IsDone)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","this is error");
                }
            }
            _unitOfWork.Dispose();
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
         var projectImageResult = await  _unitOfWork.projectImageRepository.GetByIdAsync(id);
            if (projectImageResult==null)
            {

            }
          var projectResult= await _unitOfWork.projectRepository.GetByIdAsync(projectImageResult.ProjectId);
          var ImageResult  = await _unitOfWork.projectRepository.GetByIdAsync(projectImageResult.ImageId);
            if (ImageResult!=null&&projectImageResult!=null)
            {
            var projectUpdateResult=    _unitOfWork.projectRepository.Delete(projectResult);
            

            }
            return View();
        }
    }
}
