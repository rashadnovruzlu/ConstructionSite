using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.News;
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
    [Authorize(Roles = ROLESNAME.Admin)]
    public class BlogController : Controller
    {
        #region Fields

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlogFacade _blogFacade;
        private readonly IBlogImageFacade _blogImageFacade;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly ConstructionDbContext _dbContext;

        #endregion Fields

        #region CTOR

        public BlogController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor,
                                 ConstructionDbContext dbContext,
                                 IBlogFacade blogFacade,
                                 IBlogImageFacade blogImageFacade)
        {
            _unitOfWork = unitOfWork;
            _blogFacade = blogFacade;
            _blogImageFacade = blogImageFacade;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _dbContext = dbContext;
            _lang = _httpContextAccessor.GetLanguages();
        }

        #endregion CTOR

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
                                                NewsId = x.News.Id,
                                                Title = x.News.FindTitle(_lang),
                                                Content = x.News.FindContent(_lang),
                                                Imagepath = x.Image.Path,
                                                CreateDate = x.News.CreateDate,
                                            }).OrderByDescending(x => x.NewsId)
                                            .ToList();
            if (newsImageResult.Count < 1 | newsImageResult == null)
            {
                ModelState.AddModelError("", "Data is null or Empty");
            }
            return View(newsImageResult);
        }

        #endregion INDEX

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
        public async Task<IActionResult> Create(BlogAddViewModel blogAddViewModel)
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

            var resulBlogAddViewModel = await _blogFacade.Add(blogAddViewModel);
            var resultImage = await blogAddViewModel.file.SaveImageCollectionAsync(_env, "news", _unitOfWork);
            if (resulBlogAddViewModel.IsDone && resultImage.Count > 0)
            {
                foreach (var item in resultImage)
                {
                    var result = new NewsImageAddViewModel
                    {
                        ImageID = item,
                        NewsID = resulBlogAddViewModel.Data.Id
                    };
                    await _blogImageFacade.Add(result);
                }
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        #endregion CREATE

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
        public async Task<IActionResult> Edit(BlogEditModel blogEditModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (blogEditModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
            }
            var resultUpdateNews = await _blogFacade.Update(blogEditModel);
            await UpdateAll(blogEditModel, resultUpdateNews);
            if (await _unitOfWork.CommitAsync())
            {
                return RedirectToAction("Index");
            }
            return View(blogEditModel.Id);


        }

        private async Task UpdateAll(BlogEditModel blogEditModel, RESULT<News> result)
        {
            if (blogEditModel.file != null)
            {
                var resultImaage = _unitOfWork.newsImageRepository.GetAll()
              .Where(x => x.NewsId == blogEditModel.NewsId)
              .Take(blogEditModel.file.Count)
              .Select(x => x.Image).ToArray();
                if (result.IsDone && blogEditModel.file.Count > 0)
                {
                    _env.Delete(resultImaage, "blog", _unitOfWork);
                    var resultImageAdd = await blogEditModel.file.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
                    foreach (var item in resultImageAdd)
                    {
                        NewsImage newsImage = new NewsImage
                        {
                            ImageId = item,
                            NewsId = result.Data.Id
                        };
                        _unitOfWork.newsImageRepository.Update(newsImage);
                    }
                }
            }
        }

        #endregion UPDATE

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

            if (newsResult != null && imageResult != null)
            {
                var newsDeleteResult = await _unitOfWork.newsRepository
                                                   .DeleteAsync(newsResult);
                var imageDeleteResult = ImageExtensions.DeleteAsyc(_env, imageResult, "news", _unitOfWork);
                if (newsDeleteResult.IsDone && imageDeleteResult)
                {
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "delete error");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}