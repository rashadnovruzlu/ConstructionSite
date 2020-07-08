using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.Description;
using ConstructionSite.Entity.Models;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class DescriptionController : Controller
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public DescriptionController(IUnitOfWork unitOfWork,
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
            var result=_unitOfWork.descriptionRepstory.GetAll()
                .Select(x=>new DescriptionViewModel
                {
                    Id=x.Id,
                    Tittle=x.FindTitle(_lang),
                    Content=x.FindContent(_lang)
                }).ToList();
            if (result==null|result.Count==0)
            {
                return Json(new
                {
                    message = "Description is null or empty"
                });
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });


            }
            var result = _unitOfWork.SubServiceRepository.GetAll()
                 .Select(x => new DescriptionSubServer
                 {
                     Id = x.Id,
                     Name = x.FindName(_lang)
                 }).ToList();
            if (result == null| result.Count<0)
            {
                return Json(new
                {
                    message = "SubService is empty"
                });
            }
            ViewBag.items = result;
            _unitOfWork.Dispose();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DescriptionAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });


            }
            if (model==null)
            {
                return Json(new
                {
                    message = "description add model is null"
                });
            }
            Description Descriptionresult = new Description
            {
                Id=model.Id,
                TittleAz=model.TittleAz,
                TittleRu=model.TittleRu,
                TittleEn=model.TittleEn,
                ContentAz=model.ContentAz,
                ContentRu=model.ContentRu,
                ContentEn=model.ContentEn,
                SubServiceId=model.SubServiceID
            };
          var isResult=await _unitOfWork.descriptionRepstory.AddAsync(Descriptionresult);
            if (isResult.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            var result = _unitOfWork.SubServiceRepository.GetAll()
                .Select(x => new DescriptionSubServer
                {
                    Id = x.Id,
                    Name = x.FindName(_lang)
                }).ToList();
            if (result == null | result.Count < 0)
            {
                return Json(new
                {
                    message = "SubService is empty"
                });
            }
            ViewBag.items = result;
            _unitOfWork.Rollback();
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });


            }
            if (id == 0)
            {
                return Json(new
                {
                    message = "id is null"
                });
            }
            var descriptionUpdateViewModel=  _unitOfWork.descriptionRepstory.GetById(id);
            if (descriptionUpdateViewModel==null)
            {
                return Json(new
                {
                    message= "DescriptionUpdateViewModel is empty"
                });
            }
            var result=new DescriptionAddViewModel
           {
               Id      =descriptionUpdateViewModel.Id,
               TittleAz=descriptionUpdateViewModel.TittleAz,
               TittleRu=descriptionUpdateViewModel.TittleRu,
               TittleEn=descriptionUpdateViewModel.TittleEn,
               ContentAz=descriptionUpdateViewModel.ContentAz,
               ContentRu=descriptionUpdateViewModel.ContentRu,
               ContentEn=descriptionUpdateViewModel.ContentEn,
               SubServiceID=descriptionUpdateViewModel.SubServiceId
           };
            _unitOfWork.Dispose();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(DescriptionUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });


            }
            if (model==null)
            {
                return Json(new
                {
                    message = "DescriptionUpdateViewMode is null"
                });
            }
            var DescriptionUpdateViewModel=new Description
            {
                Id = model.Id,
                TittleAz = model.TittleAz,
                TittleRu = model.TittleRu,
                TittleEn = model.TittleEn,
                ContentAz = model.ContentAz,
                ContentRu = model.ContentRu,
                ContentEn = model.ContentEn,
                SubServiceId = model.SubServiceID

            };
            var result=  _unitOfWork.descriptionRepstory.Update(DescriptionUpdateViewModel);
            if (result.IsDone)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _unitOfWork.Rollback();
            }
            _unitOfWork.Dispose();
            return View(model.Id);
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(new
                {
                    message = "BadRequest"
                });


            }
            if (id==0)
            {
                return Json(new
                {
                    message = "id is null"
                });
            }
             var resultbyId= _unitOfWork.descriptionRepstory.GetById(id);
            if (resultbyId==null)
            {
                return Json(new
                {
                    message = "id is null"
                });

            }
           var result= _unitOfWork.descriptionRepstory.Delete(resultbyId);
            if (result.IsDone)
            {
                _unitOfWork.Dispose();
                return RedirectToAction("Index");

            }
          
            return View();
        }
    }
}
