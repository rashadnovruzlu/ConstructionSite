﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.ModelsDTO;
using ConstructionSite.Entity.Data;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private string                        _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IWebHostEnvironment  _env;
        private readonly ConstructionDbContext _dbContext;
        public BlogController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor, 
                                 ConstructionDbContext dbContext)
        {

            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _dbContext = dbContext;
            _lang = _httpContextAccessor.getLang();

        }
        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            var result = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                            .Select(x => new NewsDTO
                                            {
                                                Id = x.News.Id,
                                                Tittle = x.News.FindTitle(_lang),
                                                Content = x.News.FindContent(_lang),
                                                Image = x.Image.Path,
                                                CreateDate = x.News.CreateDate
                                            }).ToList();
            if (result.Count < 1 | result==null)
            {
                ModelState.AddModelError("", "Data is null or Empty");
            }
            return View(result);
        }
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //        return Json(new
        //        {
        //            message = "BadRequest"
        //        });


        //    }
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult Add(string str)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;

        //        return Json(new
        //        {
        //            message = "BadRequest"
        //        });


        //    }
        //    return View();
        //}
    }
}
