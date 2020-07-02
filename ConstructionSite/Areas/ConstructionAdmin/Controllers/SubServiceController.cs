using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Concreate;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    public class SubServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.data=_unitOfWork.ServiceRepository.GetAll().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(SubService subService)
        {
            return View();
        }
    }
}
