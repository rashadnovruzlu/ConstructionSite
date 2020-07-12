using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        #region Fields
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        #endregion

        #region CTOR
        public ProjectController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.getLang();
        }
        #endregion

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var result = _unitOfWork.projectImageRepository.GetAll()
            .Include(x => x.Image)
            .Include(x => x.Project)
            .Select(x => new ProjectViewModel
            {
                Id = x.Id,
                Name = x.Project.FindName(_lang),
                Content = x.Project.FindContent(_lang),
                Image = x.Image.Path,
                ImageId = x.ImageId
            }).ToList();
            if (result == null | result.Count == 0)
            {
                ModelState.AddModelError("", "This list empty");
            }
            _unitOfWork.Dispose();
            return View(result);
        }

        #endregion

        #region CREATE

        [HttpGet]
        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            var portfolioResult = _unitOfWork.portfolioRepository.GetAll()
                .Select(x => new PortfolioViewModel
                {
                    Id = x.Id,
                    Name = x.FindName(_lang)
                }).ToList();
            if (portfolioResult != null)
            {
                ViewBag.items = portfolioResult;
            }
            else
            {
                ModelState.AddModelError("", "This list is empty");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectAddModel projectAddModel, IFormFile file)
        {
            Image img = new Image();
            int imageID = 0;
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (file is null)
            {
                ModelState.AddModelError("", "NULL");
            }

            var projectAddModelresult = new Project
            {
                NameAz = projectAddModel.NameAz,
                NameRu = projectAddModel.NameRu,
                NameEn = projectAddModel.NameEn,
                ContentAz = projectAddModel.ContentAz,
                ContentRu = projectAddModel.ContentRu,
                ContentEn = projectAddModel.ContentEn,
                PortfolioId = projectAddModel.PortfolioId,
            };
            var projectResult = await _unitOfWork.projectRepository.AddAsync(projectAddModelresult);
            if (projectResult.IsDone)
            {
                imageID = await file.SaveImage(_env, "project", img, _unitOfWork);
                if (imageID < 1)
                {
                    ModelState.AddModelError("", "File is not saved.");
                }
                ProjectImage image = new ProjectImage
                {
                    ImageId = imageID,
                    ProjectId = projectAddModelresult.Id
                };
                var imageResult = await _unitOfWork.projectImageRepository.AddAsync(image);
                if (imageResult.IsDone)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This is error");
                }
            }
            _unitOfWork.Dispose();
            return View();
        }

        #endregion

        #region UPDATE

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "Id is not exists");
            }
            ViewBag.items = _unitOfWork.projectRepository.GetAll();
            _unitOfWork.Dispose();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProjectUpdateViewModel projectUpdateViewModel, IFormFile file)
        {
            Image image = new Image();

            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (projectUpdateViewModel == null)
            {
                ModelState.AddModelError("", "Data is null");
            }

            var projectViewModelUpdate = new Project
            {
                Id = projectUpdateViewModel.Id,
                NameAz = projectUpdateViewModel.NameAz,
                NameRu = projectUpdateViewModel.NameRu,
                NameEn = projectUpdateViewModel.NameEn,
                ContentAz = projectUpdateViewModel.ContentAz,
                ContentRu = projectUpdateViewModel.ContentRu,
                ContentEn = projectUpdateViewModel.ContentEn,
                PortfolioId = projectUpdateViewModel.PortfolioId
            };
            var portfolioUpdateResult = await _unitOfWork.projectRepository.UpdateAsync(projectViewModelUpdate);
            if (!portfolioUpdateResult.IsDone)
            {
                ModelState.AddModelError("", "Errors occured while editing Portfolio");
            }
            if (file is null)
            {
                ModelState.AddModelError("", "File is not exists");
            }
            var imageResult = await file.UpdateAsyc(_env, image, "project", _unitOfWork);
            if (!imageResult)
            {
                ModelState.AddModelError("", "Errors occured while editing Images");
            }
            ProjectImage projectImage = new ProjectImage
            {
                ImageId = image.Id,
                ProjectId = projectUpdateViewModel.Id
            };
            var projectImageUpdateResult = await _unitOfWork.projectImageRepository.UpdateAsync(projectImage);
            if (!projectImageUpdateResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Updating is not valid");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var projectImageResult = await _unitOfWork.projectImageRepository.GetByIdAsync(id);
            if (projectImageResult == null)
            {
                ModelState.AddModelError("", "Id is not exists");
            }
            var projectResult = await _unitOfWork.projectRepository.GetByIdAsync(projectImageResult.ProjectId);
            var ImageResult = await _unitOfWork.projectRepository.GetByIdAsync(projectImageResult.ImageId);

            if (ImageResult != null && projectImageResult != null)
            {
                var projectUpdateResult = _unitOfWork.projectRepository.Delete(projectResult);

                _unitOfWork.Dispose();
            }
            else
            {
                ModelState.AddModelError("", "Deleting is error");
            }
            return View();
        }

        #endregion

    }
}