using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Entity.Models;
using ConstructionSite.Interfaces.Facade;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class GaleryController : CoreController
    {
        private readonly IGaleryFacade _galeryFacade;
        public GaleryController(IGaleryFacade galeryFacade)
        {
            _galeryFacade = galeryFacade;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region ::ADD::
        public IActionResult Add(Galery galery)
        {



            return View();

        }

        [HttpPost]
        public IActionResult Add(string str)
        {
            return View();
        }
        #endregion
        #region ::UPDATE::
        public IActionResult Update(int id)
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        #endregion
        #region ::DELETE::
        public IActionResult Delete(int id)
        {
            return View();
        }
        #endregion


    }
}
