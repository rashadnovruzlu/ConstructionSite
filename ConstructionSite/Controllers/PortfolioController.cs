﻿using ConstructionSite.DTO.FrontViewModels.Portfoli;
using ConstructionSite.DTO.FrontViewModels.Portfolio;
using ConstructionSite.DTO.FrontViewModels.Project;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class PortfolioController : Controller
    {

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SharedLocalizationService _sharedLocalizationService;


        public PortfolioController(IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor,
                              SharedLocalizationService sharedLocalizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _lang = _httpContextAccessor.getLang();
            _sharedLocalizationService=sharedLocalizationService;
        }
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("",_sharedLocalizationService.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var result=_unitOfWork.portfolioRepository.GetAll()
                .Select(x=>new PortfolioViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang),
                    
                    
                }).ToList();
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _sharedLocalizationService.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            if (id<1)
            {

            }
         var result=   _unitOfWork.projectImageRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.Project)
               
                .Select(x=>new ProjectViewDetailsModel
                {
                    ID=x.Project.Id,
                    Content=x.Project.FindContent(_lang),
                    Name=x.Project.FindName(_lang),
                    Images=x.Project.ProjectImages.Select(y=>y.Image.Path).ToList()

                }).FirstOrDefault(x=>x.ID==id);
            return View(result);
        }
        public PartialViewResult Project(int id=0)
        {
            if (id==0)
            {

                var Queryresult = _unitOfWork.projectImageRepository.GetAll()

                      .Include(x => x.Project)
                      .Include(x => x.Image)

                      .Select(x => new ProjectViewModel
                      {
                          Id = x.Project.Id,
                          Name = x.Project.FindName(_lang),
                          Image = x.Image.Path
                      })
                      .ToList();
                return PartialView(Queryresult);
            }
           
                var result = _unitOfWork.projectImageRepository.GetAll()

                   .Include(x => x.Project)
                   .Include(x => x.Image)
                   .Where(x => x.Project.PortfolioId == id)
                   .Select(x => new ProjectViewModel
                   {
                       Id = x.Project.Id,
                       Name = x.Project.FindName(_lang),
                       Image = x.Image.Path
                   })
                   .ToList();
                return PartialView(result);
           
           
        }
        public PartialViewResult All()
        {
             var Queryresult = _unitOfWork.projectImageRepository.GetAll()

                   .Include(x => x.Project)
                   .Include(x => x.Image)

                   .Select(x => new ProjectViewModel
                   {
                       Id = x.Project.Id,
                       Name = x.Project.FindName(_lang),
                       Image = x.Image.Path
                   })
                   .ToList();
                return PartialView(Queryresult);
           
          
        }
    }
}
