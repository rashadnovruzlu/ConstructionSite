using ConstructionSite.DTO.AdminViewModels.Blog;
using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Blogs;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IBlogQueryFacade _blogQueryFacade;
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
                                 IBlogImageFacade blogImageFacade,
                                 IBlogQueryFacade blogQueryFacade)
        {
            _unitOfWork = unitOfWork;
            _blogFacade = blogFacade;
            _blogImageFacade = blogImageFacade;
            _blogQueryFacade = blogQueryFacade;
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
            var newsImageResult = _blogFacade.GetAll(_lang);
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
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogAddViewModel blogAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index", "Home");
            }
            if (blogAddViewModel == null)
            {
                ModelState.AddModelError("", "News is empty");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var resulBlogAddViewModel = await _blogFacade.Add(blogAddViewModel);
                var resultImage = await blogAddViewModel.file.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
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
                    else
                    {
                        _unitOfWork.Rollback();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
            }
            return View();
        }

        #endregion CREATE

        #region UPDATE

        public async Task<IActionResult> Edit(int id)
        {


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index");
            }

            var result = await _blogQueryFacade.GetForUpdate(id);

            if (result == null)
            {
                ModelState.AddModelError("", "Errors occured while editing Blog Images");
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            if (blogEditModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
                return RedirectToAction("Index");
            }

            try
            {
                if (blogEditModel.files != null && blogEditModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < blogEditModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == blogEditModel.ImageID[i]);
                            await blogEditModel.files[i].UpdateAsyc(_env, image, "blog", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (blogEditModel.files != null)
                {
                    var emptyImage = _unitOfWork.newsRepository.Find(x => x.Id == blogEditModel.Id);

                    var imagesid = await blogEditModel.files.SaveImageCollectionAsync(_env, "blog", _unitOfWork);
                    foreach (var item in imagesid)
                    {
                        var resultData = new NewsImage
                        {
                            NewsId = emptyImage.Id,
                            ImageId = item
                        };
                        await _unitOfWork.newsImageRepository.AddAsync(resultData);
                    }
                    await _unitOfWork.CommitAsync();
                }
                var resultAbout = await _blogFacade.Update(blogEditModel);
                if (resultAbout)
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _unitOfWork.Rollback();
                    }
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        #endregion UPDATE

        #region DELETE

        public IActionResult Delete(int id)
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
            if (_blogFacade.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #endregion DELETE
    }
}