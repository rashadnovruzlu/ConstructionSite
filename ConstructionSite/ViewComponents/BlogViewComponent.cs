using ConstructionSite.DTO.FrontViewModels.Blog;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ConstructionSite.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SharedLocalizationService _localizationHandle;

        public BlogViewComponent(IUnitOfWork unitOfWork,
                                 IHttpContextAccessor contextAccessor,
                                 SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _localizationHandle = localizationHandle;
            _lang = _contextAccessor.getLanguages();
        }

        public IViewComponentResult Invoke()
        {
            if (!ModelState.IsValid)
            {
                _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            var newsImageResult = _unitOfWork.newsImageRepository.GetAll()
                                                .Select(x => new BlogViewModel
                                                {
                                                    Id = x.Id,
                                                    Tittle = x.News.FindTitle(_lang),
                                                    Content = x.News.FindContent(_lang),
                                                    NewsId = x.NewsId,
                                                    Image = x.Image.Path,
                                                    ImageId = x.ImageId
                                                }).OrderByDescending(x=>x.Id).Take(3)
                                                .ToList()
                                                    ;
            if (newsImageResult == null)
            {
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            return View(newsImageResult);
        }
    }
}
