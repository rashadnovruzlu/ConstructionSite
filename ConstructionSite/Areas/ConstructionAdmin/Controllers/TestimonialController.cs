using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class TestimonialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult add(string str)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
