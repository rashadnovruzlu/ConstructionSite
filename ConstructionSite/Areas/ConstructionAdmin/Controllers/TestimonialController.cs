using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
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
        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            return View();
        }
        [HttpGet]
        public IActionResult add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CustomerFeedback customerFeedback)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
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
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
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
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this data is null or empity");
            }
            _unitOfWork.Dispose();
            return View(customerFeedbackUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CustomerFeedback customerFeedback)
        {
            if (customerFeedback==null)
            {
                ModelState.AddModelError("", "this data is null or empity");
            }
            var customerFeedbackUpdateResult=await  _unitOfWork.customerFeedbackRepository.UpdateAsync(customerFeedback);
            if (!customerFeedbackUpdateResult.IsDone)
            {
                ModelState.AddModelError("", "this data is null or empity");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });
            }
            if (id<1)
            {
                ModelState.AddModelError("", "id is null");

            }
            var customerFeedbackResult= await _unitOfWork.customerFeedbackRepository.GetByIdAsync(id);
            if (customerFeedbackResult!=null)
            {
                ModelState.AddModelError("", "this data is null or empity");
            }
            var customerFeedbackDeleteResult=await  _unitOfWork.customerFeedbackRepository.DeleteAsync(customerFeedbackResult);
            if (!customerFeedbackDeleteResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "this data is null or empity");
            }
            _unitOfWork.Dispose();
            return View();
        }
    }
}
