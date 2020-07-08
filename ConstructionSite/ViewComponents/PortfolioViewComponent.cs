using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class PortfolioViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        string _lang;
        public PortfolioViewComponent(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork=unitOfWork;
            _httpContext = httpContext;
            _lang = _httpContext.getLang();
        }
        public IViewComponentResult Invoke()
        {
            /* var result = _unitOfWork.projectRepository.GetAll()
                                       .Include(x => x.Portfolio)
                                           .Include(x => x.ProjectImages)
                                               .Select(y => new PortfolioViewModel
                                               {
                                                   Name = y.Portfolio.FindName(_lang),
                                                   Id = y.Portfolio.Id
                                               }).ToList();  */

            var result = _unitOfWork.projectRepository.GetAll()
                                    .Include(x => x.Portfolio)
                                        .Select(x => new PortfolioViewModel
                                        {
                                            Name = x.Portfolio.FindName(_lang),
                                            ProjectViewModel = new List<ProjectViewModel>
                                            {
                                                new ProjectViewModel
                                                {
                                                    Name=x.FindName(_lang),
                                                    Content=x.FindContent(_lang),

                                                }
                                            }
                                        });

            return View(result);
        }
    }
}
