using ConstructionSite.Repository.Abstract;
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
        public PortfolioViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IViewComponentResult Invoke()
        { 
           // _unitOfWork.projectRepository.GetAll()
               // .Include(x=>x.)
                
        return View();
        }
        }
}
