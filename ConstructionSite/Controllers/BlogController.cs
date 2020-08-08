using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.DTO.FrontViewModels.Blog;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Helpers.Paging;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    public class BlogController : Controller
    {
        /// <summary>
        /// this is include need class
        /// </summary>
        private string _lang;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private SharedLocalizationService _localizationHandle;

        /// <summary>
        /// Conustructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="localizationHandle"></param>
        public BlogController(IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor,
                              SharedLocalizationService localizationHandle)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _lang = _httpContextAccessor.getLanguages();
            _localizationHandle = localizationHandle;
        }

        /// <summary>
        /// this action for first get all list
        /// and pagination
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Index(int page = 1)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                 .Include(x => x.Image)
                 .Include(x => x.News)
                 .ToList();
            var newsViewModelResult = newsImageResult
                   .Select(x => new NewsViewModel
                   {
                       Id = x.NewsId,
                       Title = x.News.FindTitle(_lang),
                       Content = x.News.FindContent(_lang),
                       Imagepath = x.Image.Path,
                       CreateDate = x.News.CreateDate
                   })
                  .ToList()
                  .Skip((page - 1) * 3)
                  .Take(3)
                  .AsEnumerable();
            var paginModelResult = new PaginModel<NewsViewModel>()
            {
                Paging = newsViewModelResult,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPrePage = 3,
                    TotalItems = newsImageResult.Count()
                }
            };
            if (newsImageResult == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(paginModelResult);
        }

        /// <summary>
        /// this action only for detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }
            var newsImageResult = _unitOfWork.newsImageRepository
                .GetAll()
                .Include(x => x.News)
                .Include(x => x.Image)
                .FirstOrDefault(x => x.NewsId == id);

            if (newsImageResult == null)
            {
                return RedirectToAction("Index");
            }
            var blogDetalyeViewModel = new BlogDetalyeViewModel
            {
                Id = newsImageResult.NewsId,
                Title = newsImageResult.News.FindTitle(_lang),
                Content = newsImageResult.News.FindContent(_lang),
                dateTime = newsImageResult.News.CreateDate,
                imagePath = newsImageResult.Image.Path,
            };
            return View(blogDetalyeViewModel);
        }
    }
}