using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        { 
            return View();
        }
    }
}
