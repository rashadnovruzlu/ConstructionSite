using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork         _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ProjectController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {

            _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
         ViewBag.items=  _unitOfWork.portfolioRepository.GetAll().ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProjectAddModel project,IFormFile file)
        {
            Image img = new Image();
            int IMAGEID=0;
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
            IMAGEID = await file.SaveImage(_env, "project", img, _unitOfWork);
            if (IMAGEID<0)
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
                    ImageId = IMAGEID,
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
