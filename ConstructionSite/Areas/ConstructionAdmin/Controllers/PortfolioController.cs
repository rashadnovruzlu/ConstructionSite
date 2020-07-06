using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.Areas.ConstructionAdmin.Models.ViewModels;
using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PortfolioController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
           _lang= _httpContextAccessor.getLang();
        }
        public  IActionResult Index()
        {

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
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
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });

            }
            if (portfolio is null)
            {
                return Json(new
                {
                    message = "data is null"
                });
            }
            var portfolioResult= await _unitOfWork.portfolioRepository.AddAsync(portfolio);
            if (portfolioResult.IsDone)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Data Not Save");
            }
            return View();
        }
    }
}
