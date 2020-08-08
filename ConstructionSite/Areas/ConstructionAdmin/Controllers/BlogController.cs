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
            _lang = _httpContextAccessor.getLanguages();
        }
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                            .Select(x => new BlogViewModel
                                            {
                                                Id = x.Id,
                                                NewsId=x.News.Id,
                                                Title = x.News.FindTitle(_lang),
                                                Content = x.News.FindContent(_lang),
                                                Imagepath = x.Image.Path,
                                                CreateDate = x.News.CreateDate,
                                            }).OrderByDescending(x=>x.NewsId)
                                            .ToList();
            if (newsImageResult.Count < 1 | newsImageResult == null)
            {
                ModelState.AddModelError("", "Data is null or Empty");
            }
            return View(newsImageResult);
        }

        #endregion

        #region CREATE

        [HttpGet]
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "ModelState is not valid.");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogAddViewModel blogAddViewModel, IFormFile file)
        {
            Image image = new Image();
            
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (blogAddViewModel == null)
            {
                ModelState.AddModelError("", "News is empty");
            }
            var newsAddModelResult = new News
            {
                Id        = blogAddViewModel.Id,
                TittleAz  = blogAddViewModel.TittleAz,
                TittleEn  = blogAddViewModel.TittleEn,
                TittleRu  = blogAddViewModel.TittleRu,
                ContentAz = blogAddViewModel.ContentAz,
                ContentEn = blogAddViewModel.ContentEn,
                ContentRu = blogAddViewModel.ContentRu,
                CreateDate = DateTime.Now
            };
            var addNewViewResult = await _unitOfWork.newsRepository.AddAsync(newsAddModelResult);
            if (!addNewViewResult.IsDone)
            {
                _unitOfWork.Dispose();
                ModelState.AddModelError("", "Errors occured while creating News");
            }

            var addImageViewResult = await file.SaveImage(_env, "News", image, _unitOfWork);
            if (!addImageViewResult)
            {
                ImageExtensions.DeleteAsyc(_env, image, "News", _unitOfWork);
                ModelState.AddModelError("", "Errors occured while creating Images");
            }
            NewsImage newsImageData = new NewsImage
            {
              ImageId = image.Id,
              NewsId = newsAddModelResult.Id
           };
           
            var newsImageResult = await _unitOfWork.newsImageRepository.AddAsync(newsImageData);
            if (!newsImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while creating News Images");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region UPDATE

        public IActionResult Edit(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("", "This data is not exists");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }

            var result = _unitOfWork.newsImageRepository.GetAll()
                                    .Include(x => x.News)
                                        .Include(x => x.Image)
                                        
                                            .Select(x => new BlogEditModel
                                            {
                                                Id = x.Id,
                                                TittleAz = x.News.TittleAz,
                                                TittleEn = x.News.TittleEn,
                                                TittleRu = x.News.TittleRu,
                                                ContentAz = x.News.ContentAz,
                                                ContentEn = x.News.ContentEn,
                                                ContentRu = x.News.ContentRu,
                                                Image = x.Image.Path,
                                                ImageId = x.Image.Id,
                                                NewsId = x.News.Id
                                            }).FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                ModelState.AddModelError("", "Errors occured while editing Blog Images");
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogEditModel blogEditModel, IFormFile file)
        {
           
           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
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
            if (file!=null)
            {
                var imageResult = await _unitOfWork.imageRepository.GetByIdAsync(blogEditModel.ImageId);
                if (imageResult==null)
                {
                    ModelState.AddModelError("","file is null");
                }
                var resultUpdateAsyc = await file.UpdateAsyc(_env, imageResult, "News", _unitOfWork);
                if (!resultUpdateAsyc)
                {
                    ModelState.AddModelError("", "File is NULL");
                }
              
               
              
            }
         



            var newsImageSelectResult = await _unitOfWork.newsImageRepository.GetByIdAsync(blogEditModel.Id);
            if (newsImageSelectResult==null)
            {
                ModelState.AddModelError("", "data is NULL");
            }
           newsImageSelectResult.NewsId=resultViewModel.Id;
           newsImageSelectResult.ImageId= blogEditModel.ImageId;
         
            var result = await _unitOfWork.newsImageRepository.UpdateAsync(newsImageSelectResult);
            if (!result.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("","data can be update");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "Object is NULL");
            }
            var newsImageResult = await _unitOfWork.newsImageRepository
                                                    .GetByIdAsync(id);
            if (newsImageResult == null)
            {
                ModelState.AddModelError("", "Data is NULL");
                return RedirectToAction("Index");
            }
            var newsResult = await _unitOfWork.newsRepository
                                                .GetByIdAsync(newsImageResult.NewsId);
            
           var imageResult = await _unitOfWork.imageRepository
                                            .GetByIdAsync(newsImageResult.ImageId);

            if (newsResult!=null&&imageResult!=null)
            {
                var newsDeleteResult = await _unitOfWork.newsRepository
                                                   .DeleteAsync(newsResult);
               var imageDeleteResult= ImageExtensions.DeleteAsyc(_env, imageResult, "news", _unitOfWork);
                if (newsDeleteResult.IsDone&&imageDeleteResult)
                {
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","delete error");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}