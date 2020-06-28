using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    

    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AboutController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
