using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUnitOfWork _unitOfWork;
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

            if (ModelState.IsValid)
            {
                Image img = new Image();
                int id = await file.SaveImage(_env, "project", img, _unitOfWork);
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
              await  _unitOfWork.projectRepository.AddAsync(result);
                ProjectImage image = new ProjectImage
                {
                    ImageId = id,
                    ProjectId=result.Id
                };
                if (await _unitOfWork.projectImageRepository.AddAsync(image)>0)
                    return RedirectToAction("Index");
                    _unitOfWork.Dispose();
                
               
            }


            return View();
        }
    }
}
