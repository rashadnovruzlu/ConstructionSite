using ConstructionSite.DTO.AdminViewModels;
using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Encodings;
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
            return View();
        }
    }
}
