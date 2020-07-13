using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.DTO.FrontViewModels.Portfoli;
using ConstructionSite.DTO.FrontViewModels.Portfolio;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class PortfolioViewComponent:ViewComponent
    {
        private readonly IUnitOfWork          _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private    string                     _lang;
        public PortfolioViewComponent(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork=unitOfWork;
            _httpContext = httpContext;
            _lang = _httpContext.getLang();
        }
        public IViewComponentResult Invoke()
        {
            var result = _unitOfWork.portfolioRepository.GetAll()
                                    .Include(x => x.Projects)
                                        .Select(x => new PortfolioViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.FindName(_lang),
                                            Projects = x.Projects.Select(y => new ProjectMenuViewModel
                                            {
                                                Id = y.Id,
                                                Name = y.FindName(_lang)
                                            }).ToList()
                                        }).ToList();

                return View(result);
        }
    }
}
