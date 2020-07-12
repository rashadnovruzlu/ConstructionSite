using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = ROLESNAME.Admin)]
    public class BlogController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly ConstructionDbContext _dbContext;
        #endregion

        #region CTOR
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
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid.");
            }
            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                            .Select(x => new BlogViewModel
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

        #endregion Index

        #region CREATE

        [HttpGet]
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogAddViewModel newsAddModel, IFormFile file)
        {
            Image image = new Image();
            NewsImage newsImage = new NewsImage();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (newsAddModel == null)
            {
                ModelState.AddModelError("", "MNews is empty.");
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
                CreateDate = DateTime.Now
            };
            var addNewViewResult = await _unitOfWork.newsRepository.AddAsync(newsAddModelResult);
            if (!addNewViewResult.IsDone)
            {
                _unitOfWork.Dispose();
                ModelState.AddModelError("", "Errors occured while creating News");
            }

            var addImageViewResult = await file.SaveImage(_env, "News", image, _unitOfWork);
            if (addImageViewResult == 0)
            {
                ImageExtensions.DeleteAsyc(_env,image,"News",_unitOfWork);
                ModelState.AddModelError("", "Image add samo errors");
                ModelState.AddModelError("", "Errors occured while creating Image");
            }
            newsImage.ImageId = image.Id;
            newsImage.NewsId = newsAddModelResult.Id;
            var newsImageResult = await _unitOfWork.newsImageRepository.AddAsync(newsImage);
            if (!newsImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while creating News Images");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion Create

        #region EDIT

        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data is not exists");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model State is not Valid.");
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

            if (result == null)
            {
                ModelState.AddModelError("", "Errors occured while editing News");
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditModel blogEditModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (blogEditModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }
            var resultViewModel = new News
            {
                Id = blogEditModel.NewsId,
                ContentAz = blogEditModel.ContentAz,
                ContentEn = blogEditModel.ContentEn,
                ContentRu = blogEditModel.ContentRu,
                TittleAz = blogEditModel.TittleAz,
                TittleEn = blogEditModel.TittleEn,
                TittleRu = blogEditModel.TittleRu,
                CreateDate = blogEditModel.DateTime
            };
            var newsResult = await _unitOfWork.newsRepository.UpdateAsync(resultViewModel);
            if (newsResult == null)
            {
            }
            var imageResult = await _unitOfWork.imageRepository.GetByIdAsync(blogEditModel.ImageId);
            if (imageResult == null)
            {
                if (file != null)
                {
                    var resultUpdateAsyc = await file.UpdateAsyc(_env, imageResult, "News", _unitOfWork);
                    if (!resultUpdateAsyc)
                    {
                        ModelState.AddModelError("","file is null");
                    }
                }
            }
            NewsImage newsImage = new NewsImage
            {
                NewsId = blogEditModel.NewsId,
                ImageId = blogEditModel.ImageId
            };
            var result = await _unitOfWork.newsImageRepository.UpdateAsync(newsImage);
            if (!result.IsDone)
            {
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion Edit

        #region DELETE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "Model State is not Valid.");
            }
            if (id < 0)
            {
                ModelState.AddModelError("", "NULL");
            }
            var newsImageResult = await _unitOfWork.newsImageRepository
                                                    .GetByIdAsync(id);
            if (newsImageResult == null)
            {
                ModelState.AddModelError("", "News is NULL");
            }
            var newsResult = await _unitOfWork.newsRepository
                                                .GetByIdAsync(newsImageResult.NewsId);
            if (newsResult == null)
            {
                ModelState.AddModelError("", "News is NULL");
            }
            var newsDeleteResult = await _unitOfWork.newsRepository
                                                    .DeleteAsync(newsResult);
            if (!newsDeleteResult.IsDone)
            {
                ModelState.AddModelError("", "The News could not be deleted");
            }
            var image = await _unitOfWork.imageRepository
                                            .GetByIdAsync(newsImageResult.ImageId);
            if (image == null)
            {
                ModelState.AddModelError("", "Image is empty");
            }
            var imageResult = await _unitOfWork.imageRepository
                                                .DeleteAsync(image);
            if (!imageResult.IsDone)
            {
                _unitOfWork.Rollback();
            }

            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}