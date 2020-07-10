using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private string   _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public NewsController(IUnitOfWork unitOfWork,
                               IWebHostEnvironment env,
                               IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new {
                    message = "BadRequest"
                });
            }
                var newsViewModel=await _unitOfWork.newsImageRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.News)
                .Select(x=>new NewsViewModel
                {
                    Id=x.Id,
                    Content=x.News.FindContent(_lang),
                    Title=x.News.FindTitle(_lang),
                    Imagepath=x.Image.Path,
                    CreateDate=x.News.CreateDate

                }).ToListAsync();
            if (newsViewModel==null|newsViewModel.Count<0)
            {
                return Json(new
                {
                    message = "newsViewModel is empty"
                });
            }
            return View(newsViewModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewsAddModel newsAddModel,IFormFile file)
        {
            Image image=new Image();
            NewsImage newsImage=new NewsImage();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (newsAddModel==null)
            {
                return Json(new
                {
                    message = "this is emty"
                });
            }
            var newsAddModelResult=new News
            {
                Id=newsAddModel.Id,
                TittleAz=newsAddModel.TittleAz,
                TittleEn=newsAddModel.TittleEn,
                TittleRu=newsAddModel.TittleRu,
                ContentAz=newsAddModel.ContentAz,
                ContentEn=newsAddModel.ContentEn,
                ContentRu=newsAddModel.ContentRu,
                CreateDate=newsAddModel.CreateDate
            };
            var addNewViewResult=await  _unitOfWork.newsRepository.AddAsync(newsAddModelResult);
            if (!addNewViewResult.IsDone)
            {
                _unitOfWork.Dispose();
                ModelState.AddModelError("","news add samo errors");
            }
            newsImage.NewsId=newsAddModelResult.Id;
            var addImageViewResult=await file.SaveImage(_env,"News",image,_unitOfWork);
            if (addImageViewResult==0|addImageViewResult<0)
            {
                ModelState.AddModelError("", "Image add samo errors");
            }
            newsImage.ImageId=addImageViewResult;
            var newsImageResult=  await _unitOfWork.newsImageRepository.AddAsync(newsImage);
            if (!newsImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("","new image added error");
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
           var newsImageResult=  await  _unitOfWork.newsImageRepository.GetByIdAsync(id);
            if (newsImageResult==null)
            {


            }
            return View();
        }
    }
}
