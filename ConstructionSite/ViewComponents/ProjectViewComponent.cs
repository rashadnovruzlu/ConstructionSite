﻿using ConstructionSite.DTO.FrontViewModels.Project;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class ProjectViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private string _lang;

        public ProjectViewComponent(IUnitOfWork unitOfWork,
                                      IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
            _lang = _httpContext.GetLanguages();
        }

        public IViewComponentResult Invoke(int id)
        {
            var ResultView = _unitOfWork.projectImageRepository.GetAll()
                  .Where(x => x.Project.PortfolioId == id)
                  .Include(x => x.Image)
                  .Include(x => x.Project)
                  .Select(x => new ProjectViewModel
                  {
                      Id = x.Project.Id,
                      Content = x.Project.FindContent(_lang),
                      Name = x.Project.FindName(_lang),
                      Image = x.Image.Path
                  })
                  .ToList();
            return View(ResultView);
        }
    }
}