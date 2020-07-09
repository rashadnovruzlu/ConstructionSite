﻿using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace ConstructionSite.ViewComponents
{
    public class ServiceViewComponent:ViewComponent
    {
        string                                    _lang;
        private readonly IUnitOfWork              _unitOfWork;
        private readonly IHttpContextAccessor    _httpContextAccessor;
        public ServiceViewComponent(IUnitOfWork unitOfWork, 
                                    IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork=unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang=httpContextAccessor.getLang();
        }
        public  IViewComponentResult Invoke()
        {
            if (!ModelState.IsValid)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ModelState.AddModelError("", "BadRequest");
            }
                var result=  _unitOfWork.ServiceRepository.GetAll()
                .Include(x=>x.Image)
                .Include(x=>x.SubServices).Select(x=>new ServiceViewModel
                {
                    Id=x.Id,
                    image=x.Image.Path,
                    Name=x.FindName(_lang),
                    Tittle=x.FindName(_lang),
                   
                    
                }).ToList();
            if (result.Count==0|result==null)
            {
                ModelState.AddModelError("","data not exists");
            }
         return View(result);
                
                
        }
    }
}
