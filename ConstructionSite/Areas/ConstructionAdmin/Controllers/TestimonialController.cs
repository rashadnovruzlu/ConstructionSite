using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    public class TestimonialController : Controller
    {

        private string   _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public TestimonialController(IUnitOfWork unitOfWork,
                                     IWebHostEnvironment env,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CustomerFeedback customerFeedback)
        {
            if (customerFeedback==null)
            {
              ModelState.AddModelError("","this data is null or emoty");
            }
           var customerFeedbackResult=await  _unitOfWork.customerFeedbackRepository.AddAsync(customerFeedback);

            if (!customerFeedbackResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this data is not add");
            }
            _unitOfWork.Dispose();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
          var customerFeedbackUpdateResult=await  _unitOfWork.customerFeedbackRepository.GetByIdAsync(id);
            if (customerFeedbackUpdateResult==null)
            {
                ModelState.AddModelError("", "this data is null or emoty");
            }
           var customerFeedbackUpdate=new CustomerUpdateModel
           {
               Id=customerFeedbackUpdateResult.Id,
               ContentAz=customerFeedbackUpdateResult.ContentAz,
               ContentRu=customerFeedbackUpdateResult.ContentRu,
               ContentEn=customerFeedbackUpdateResult.ContentEn,
               FullName=customerFeedbackUpdateResult.FullName,
               Position=customerFeedbackUpdateResult.Position
           };
            if (customerFeedbackUpdate==null)
            {
                ModelState.AddModelError("", "this data is null or empity");
            }
            return View(customerFeedbackUpdate);
        }
        public IActionResult Update()
        {
            return View();
        }
    }
}
