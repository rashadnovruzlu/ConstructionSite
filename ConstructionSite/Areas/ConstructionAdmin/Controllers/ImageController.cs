using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class ImageController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
