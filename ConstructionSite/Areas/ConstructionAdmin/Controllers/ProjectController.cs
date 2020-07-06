using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
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
                var result= _unitOfWork.projectRepository
                .GetAll()
                .Include(x=>x.Portfolio)
                .Include(x=>x.ProjectImages)
                .Select(x=>new ProjectViewModel
                {
                    Name=x.FindName(_lang),
                    Content=x.FindContent(_lang),
                    Portfolio=new PortfolioViewModel
                    {
                        Id=x.Portfolio.Id,
                        Name=x.Portfolio.FindName(_lang)
                    }
                    

                }).ToList();
            if (result.Count>0)
            {
                return View(result);
            }
            else
            {
                ModelState.AddModelError("", "this list emity");
            }
            return View();
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
            var portfolioResult=  _unitOfWork.portfolioRepository.GetAll().ToList();
            if (portfolioResult.Count>0)
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
            imageID = await file.SaveImage(_env, "project", img, _unitOfWork);
            if (imageID<0)
            {
                return Json(new
                {
                    message = "file not save"
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
    }
}
