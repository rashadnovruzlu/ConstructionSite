﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.DTO.FrontViewModels.Blog;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace ConstructionSite.Controllers
{
    public class BlogController : Controller
    {

        private string                        _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork          _unitOfWork;
      

        public BlogController(IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _lang = _httpContextAccessor.getLang();
        }

        public async Task<IActionResult> Index(int id)
        {
            var result=await _unitOfWork.newsImageRepository.GetByIdAsync(id);
          
            
            return View();
        }
        public async Task<IActionResult> Detalye(int id)
        {
            var newsImageResult=await _unitOfWork.newsImageRepository.GetByIdAsync(id);

            var blogDetalyeViewModel = new BlogDetalyeViewModel
            {
                Id=newsImageResult.Id,
                Title=newsImageResult.News.FindTitle(_lang),
                Content=newsImageResult.News.FindContent(_lang),
                dateTime=newsImageResult.News.CreateDate,
               
                imagePath=newsImageResult.Image.Path,
                
                
            }
;
            return View(blogDetalyeViewModel);
        }

        public IActionResult Deaty()
        {
            return View();
        }
    }
}
