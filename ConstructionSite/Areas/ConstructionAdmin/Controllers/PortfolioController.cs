using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
  
    public class PortfolioController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PortfolioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
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
