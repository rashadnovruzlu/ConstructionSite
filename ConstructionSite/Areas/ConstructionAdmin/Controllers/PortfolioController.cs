using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels;
using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
  
    public class PortfolioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private string _lang;

        public PortfolioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.Culture;
            _lang = culture.Name;
        }
        public  IActionResult Index()
        {
       
          var result=  _unitOfWork.portfolioRepository.GetAll()
                .Include(x=>x.Projects)
                .Select(x=>new PortfolioViewModel
                {
                    Id=x.Id,
                    Name=x.FindName(_lang)
                }).ToList();
               
            return View(result);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
             int result=  await _unitOfWork.portfolioRepository.AddAsync(portfolio);
                if (result>0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}
