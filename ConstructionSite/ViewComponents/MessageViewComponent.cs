using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class MessageViewComponent:ViewComponent
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;


        public MessageViewComponent(IUnitOfWork unitOfWork
                                )
        {
            _unitOfWork = unitOfWork;
          
          
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
