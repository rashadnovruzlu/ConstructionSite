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

        #endregion Fields

        #region CTOR

        public ProjectController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.getLanguages();
        }

        #endregion CTOR

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
                ModelState.AddModelError("", "This list is empty");
            }
            _unitOfWork.Dispose();
            return View(result);
        }

        #endregion INDEX

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
            _unitOfWork.Dispose();
            ViewBag.data = portfolioResult;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Project project, IFormFile file)
        {
            ProjectImage projectImage = new ProjectImage();
            Image image = new Image();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
            }
            if (file == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "File is null");
            }
            var imageResultID = await file.SaveImage(_env, "project", image, _unitOfWork);

            if (!imageResultID)
            {
                ModelState.AddModelError("", "Data didn't save");
            }
            projectImage.ImageId = image.Id;
            var projectAddResult = await _unitOfWork.projectRepository
                                                        .AddAsync(project);
            if (!projectAddResult.IsDone)
            {
                ModelState.AddModelError("", "Data didn't save");
            }
            projectImage.ProjectId = project.Id;
            var projectImageResult = await _unitOfWork.projectImageRepository
                                                        .AddAsync(projectImage);
            if (!projectImageResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Errors occured while adding SubService");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index", "Project", new { Areas = "ConstructionAdmin" });
        }

        #endregion CREATE

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
            var projectUpdateViewModel = _unitOfWork.projectImageRepository
                    .GetAll()
                    .Include(x => x.Image)
                    .Include(x => x.Project)
                    .Select(x => new ProjectUpdateViewModel
                    {
                        Id = x.Id,
                        ContentAz = x.Project.ContentAz,
                        ContentEn = x.Project.ContentEn,
                        ContentRu = x.Project.ContentRu,
                        NameAz = x.Project.NameAz,
                        NameEn = x.Project.NameEn,
                        NameRu = x.Project.NameRu,
                        ImageId = x.ImageId,
                        ImagePath = x.Image.Path,

                        PortfolioId = x.Project.PortfolioId,
                        ProjectId = x.ProjectId
                    })
                    .FirstOrDefault(x => x.ProjectId == id);
            ViewBag.items = _unitOfWork.portfolioRepository.GetAll()
                     .Select(x => new PortfolioViewModel
                     {
                         Id = x.Id,
                         Name = x.FindName(_lang)
                     }).ToList();
            return View(projectUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProjectUpdateViewModel projectUpdateViewModel, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid.");
            }
            if (projectUpdateViewModel == null)
            {
                ModelState.AddModelError("", "Data is null");
                return RedirectToAction("Index");
            }

            var projectViewModelUpdate = new Project
            {
                Id = projectUpdateViewModel.ProjectId,
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
                return RedirectToAction("Index");
            }
            if (file != null)
            {
                var image = _unitOfWork.imageRepository.GetById(projectUpdateViewModel.ImageId);
                if (image == null)
                {
                    ModelState.AddModelError("", "fill is null");
                }
                var imageResult = await file.UpdateAsyc(_env, image, "project", _unitOfWork);

                if (!imageResult)
                {
                    ModelState.AddModelError("", "Errors occured while editing Images");
                }
            }

            ProjectImage projectImage = new ProjectImage
            {
                Id = projectUpdateViewModel.Id,
                ImageId = projectUpdateViewModel.ImageId,
                ProjectId = projectUpdateViewModel.Id
            };
            var projectImageUpdateResult = await _unitOfWork.projectImageRepository.UpdateAsync(projectImage);
            if (!projectImageUpdateResult.IsDone)
            {
                _unitOfWork.Rollback();
                ModelState.AddModelError("", "Updating is not valid");
                return RedirectToAction("Index");
            }
            _unitOfWork.Dispose();
            return RedirectToAction("Index");
        }

        #endregion UPDATE

        #region DELETE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var projectImageResult = await _unitOfWork.projectImageRepository.GetByIdAsync(id);
            if (projectImageResult == null)
            {
                ModelState.AddModelError("", "Id is not exists");
                return RedirectToAction("Index");
            }
            var projectResult = await _unitOfWork.projectRepository.GetByIdAsync(projectImageResult.ProjectId);
            var ImageResult = _env.Delete(projectImageResult.ImageId, "project", _unitOfWork);

            if (ImageResult != false && projectImageResult != null)
            {
                var projectDeleteResult = _unitOfWork.projectRepository.Delete(projectResult);
                if (projectDeleteResult.IsDone)
                {
                    _unitOfWork.Dispose();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Deleting is error");
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion DELETE
    }
}