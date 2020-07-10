﻿using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.DTO.ModelsDTO;
using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        #region Index

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
            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                            .Select(x => new NewsViewModel
                                            {
                                                Id = x.News.Id,
                                                Title = x.News.FindTitle(_lang),
                                                Content = x.News.FindContent(_lang),
                                                Imagepath = x.Image.Path,
                                                CreateDate = x.News.CreateDate
                                            }).ToList();
            if (newsImageResult.Count < 1 | newsImageResult == null)
            {
                ModelState.AddModelError("", "Data is null or Empty");
            }
            return View(newsImageResult);
        }


        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsAddModel newsAddModel, IFormFile file)
        {
            Image image = new Image();
            NewsImage newsImage = new NewsImage();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (newsAddModel == null)
            {
                return Json(new
                {
                    message = "this is emty"
                });
            }
            var newsAddModelResult = new News
            {
                Id = newsAddModel.Id,
                TittleAz = newsAddModel.TittleAz,
                TittleEn = newsAddModel.TittleEn,
                TittleRu = newsAddModel.TittleRu,
                ContentAz = newsAddModel.ContentAz,
                ContentEn = newsAddModel.ContentEn,
                ContentRu = newsAddModel.ContentRu,
                CreateDate = newsAddModel.CreateDate
            };
            var addNewViewResult = await _unitOfWork.newsRepository.AddAsync(newsAddModelResult);
            if (!addNewViewResult.IsDone)
            {
                _unitOfWork.Dispose();
                ModelState.AddModelError("", "news add samo errors");
            }
            newsImage.NewsId = newsAddModelResult.Id;
            var addImageViewResult = await file.SaveImage(_env, "News", image, _unitOfWork);
            if (addImageViewResult == 0 | addImageViewResult < 0)
            {
                ModelState.AddModelError("", "Image add samo errors");
            }
            newsImage.ImageId = addImageViewResult;
            var newsImageResult = await _unitOfWork.newsImageRepository.AddAsync(newsImage);
            if (!newsImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "new image added error");
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(News news, IFormFile file)
        //{
        //    if (news == null)
        //    {
        //        return View();
        //    }

        //    int imageresultID = 0;
        //    Image img = new Image();
        //    NewsImage newsImg = new NewsImage();

        //    if (!ModelState.IsValid)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Json(new
        //        {
        //            message = "Bad Request"
        //        });
        //    }
        //    var newsResult = await _unitOfWork.newsRepository.AddAsync(news);

        //    if (newsResult.IsDone)
        //    {
        //        if (file is null)
        //        {
        //            Response.StatusCode = (int)HttpStatusCode.NotExtended;
        //            return Json(new
        //            {
        //                message = "File not found"
        //            });
        //        }

        //        imageresultID = await file.SaveImage(_env, "news", img, _unitOfWork);

        //        if (imageresultID < 0)
        //        {
        //            return Json(new
        //            {
        //                message = "File not save"
        //            });
        //        }

        //        newsImg.ImageId = imageresultID;
        //        newsImg.NewsId = news.Id;

        //        var newsImageResult = await _unitOfWork.newsImageRepository.AddAsync(newsImg);

        //        if (newsImageResult.IsDone)
        //        {
        //            _unitOfWork.Dispose();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            file.Delete(_env, img, "news");

        //            _unitOfWork.Rollback();
        //            return Json(new
        //            {
        //                message = "NewsImage not save"
        //            });
        //        }
        //    }
        //    _unitOfWork.Dispose();
        //    return View();
        //}


        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data is not exists");
            }

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "The models are not true"
                });
            }

            var result = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                            .Select(y => new BlogEditModel
                                            {
                                                Id = y.News.Id,
                                                TittleAz = y.News.TittleAz,
                                                TittleEn = y.News.TittleEn,
                                                TittleRu = y.News.TittleRu,
                                                ContentAz = y.News.ContentAz,
                                                ContentEn = y.News.ContentEn,
                                                ContentRu = y.News.ContentRu,
                                                Image = y.Image.Path,
                                                ImageId = y.Image.Id,
                                                NewsId = y.NewsId
                                            }).FirstOrDefault(x => x.Id == id);
            
            if (result==null)
            {
                ModelState.AddModelError("","blog update same errors");
            }
            return View(result);
           // return RedirectToAction("Index", "Blog", new { Areas = "ConstructionAdmin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditModel editModel, IFormFile file)
        {
            if (editModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "The models are not true"
                });
            }

            News editingNews = new News
            {
                Id = editModel.Id,
                TittleAz = editModel.TittleAz,
                TittleEn = editModel.TittleEn,
                TittleRu = editModel.TittleRu,
                ContentAz = editModel.ContentAz,
                ContentEn = editModel.ContentEn,
                ContentRu = editModel.ContentRu,
            };
            var newsResult = await _unitOfWork.newsRepository
                                                .UpdateAsync(editingNews);
            if (!newsResult.IsDone)
            {
                ModelState.AddModelError("", "An error occurred while updating the news");
            }
            if (file is null)
            {
                return Json(new
                {
                    message = "No File"
                });
            }
            Image image = _unitOfWork.imageRepository
                                        .GetById(editModel.ImageId);
            if (image == null)
            {
                return Json(new 
                { 
                    message = "No Image" 
                });
            }
            var updateImageResult = await file.UpdateAsyc(_env, image, "news", _unitOfWork);
            if (updateImageResult)
            {
                ModelState.AddModelError("","this image update error");
            }
            var editingNewsImage = new NewsImage
            {
                Id = editModel.Id,
                ImageId = editModel.ImageId,
                NewsId = editingNews.Id
            };
            var newsImageResult = await _unitOfWork.newsImageRepository
                                                    .UpdateAsync(editingNewsImage);
            if (!newsImageResult.IsDone)
            {
                ModelState.AddModelError("", "The Image could not be edited");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Blog", new { Areas = "ConstructionAdmin" });
        }

        #endregion

        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (id < 0)
            {
                return Json(new
                {
                    message = "Object is null"
                });
            }
            var newsImageResult = await _unitOfWork.newsImageRepository
                                                    .GetByIdAsync(id);
            if (newsImageResult is null)
            {
                return Json(new
                {
                    message = "News is null"
                });
            }
            var newsResult = await _unitOfWork.newsRepository
                                                .GetByIdAsync(newsImageResult.NewsId);
            if (newsResult is null)
            {
                return Json(new
                {
                    message = "Data is null"
                });
            }
            var newsDeleteResult = await _unitOfWork.newsRepository
                                                    .DeleteAsync(newsResult);
            if (!newsDeleteResult.IsDone)
            {
                ModelState.AddModelError("", "The News could not be deleted");
            }
            var image = await _unitOfWork.imageRepository
                                            .GetByIdAsync(newsImageResult.ImageId);
            if (image is null)
            {
                return Json(new
                {
                    message = "No Image"
                });
            }
            var imageResult = await _unitOfWork.imageRepository
                                                .DeleteAsync(image);
            if (imageResult.IsDone)
            {
                return RedirectToAction("Index", "Blog", new { Areas = "ConstructionAdmin" });
            }
            else
            {
                ModelState.AddModelError("", "The Image could not be deleted");
            }
            _unitOfWork.Dispose();
            return View();
        }

        #endregion
    }
}
