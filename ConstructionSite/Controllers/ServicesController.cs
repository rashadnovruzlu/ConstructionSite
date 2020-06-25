using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Construction()
        {
            return View();
        }
        
        public IActionResult Renovation()
        {
            return View();
        }

        public IActionResult Consulting()
        {
            return View();
        }

        public IActionResult Architecture()
        {
            return View();
        }

        public IActionResult Electrical()
        {
            return View();
        }
    }
}