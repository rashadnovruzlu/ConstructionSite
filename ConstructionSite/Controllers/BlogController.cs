using Castle.Core.Internal;
using ConstructionSite.DTO.AdminViewModels.News;
using ConstructionSite.DTO.FrontViewModels.Blog;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                 .Include(x => x.Image)
                 .Include(x => x.News)
                 .ToList()
                 .Select(x => new NewsViewModel
                 {
                     Id = x.NewsId,
                     Title = x.News.FindTitle(_lang),
                     Content = x.News.FindContent(_lang),
                     Imagepath = x.Image.Path,
                     CreateDate = x.News.CreateDate
                 })
                 .OrderByDescending(x=>x.Id)
                 .ToList();
            return View(newsImageResult);
        }

        public async Task<IActionResult> Detalye(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Bad Request");
            }
            var newsImageResult = await _unitOfWork.newsImageRepository.GetByIdAsync(id);

            var blogDetalyeViewModel = new BlogDetalyeViewModel
            {
                Id = newsImageResult.Id,
                Title = newsImageResult.News.FindTitle(_lang),
                Content = newsImageResult.News.FindContent(_lang),
                dateTime = newsImageResult.News.CreateDate,

                imagePath = newsImageResult.Image.Path,
            };
            return View(blogDetalyeViewModel);
        }

       
    }
}