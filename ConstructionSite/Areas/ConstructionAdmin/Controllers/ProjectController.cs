using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Injections;
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Project;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionSite.Areas.ConstructionAdmin.Controllers
{
    [Area(nameof(ConstructionAdmin))]
    //[Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        #region Fields

        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectImageFacade _projectImageFacade;
        private readonly IProjectFacade _projectFacade;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        #endregion Fields

        #region CTOR

        public ProjectController(IUnitOfWork unitOfWork,
                                 IWebHostEnvironment env,
                                 IHttpContextAccessor httpContextAccessor,
                                 IProjectImageFacade projectImageFacade,
                                 IProjectFacade projectFacade)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _lang = _httpContextAccessor.GetLanguages();
            _projectFacade = projectFacade;
            _projectImageFacade = projectImageFacade;
        }

        #endregion CTOR

        #region INDEX

        [HttpGet]
        public IActionResult Index()
        {
            var result = _projectFacade.GetAll(_lang);
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
                return RedirectToAction("Index");
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
        public async Task<IActionResult> Add(Project project, List<IFormFile> file)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", "Models are not valid");
                return RedirectToAction("Index");
            }
            if (file == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                ModelState.AddModelError("", "File is null");
                return RedirectToAction("Index");
            }
            var resultProject = await _projectFacade.Add(project);
            var resultimage = await file.SaveImageCollectionAsync(_env, "project", _unitOfWork);

            return await AllSave(resultProject, resultimage);
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
                return RedirectToAction("Index");
            }
            if (id < 1)
            {
                ModelState.AddModelError("", "Id is not exists");
                return RedirectToAction("Index");
            }
            var result = _projectFacade.GetForUpdate(id);
            ViewBag.items = _unitOfWork.portfolioRepository.GetAll()
                     .Select(x => new PortfolioViewModel
                     {
                         Id = x.Id,
                         Name = x.FindName(_lang)
                     }).ToList();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProjectUpdateViewModel projectUpdateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Models are not valid.");
                return RedirectToAction("Index");
            }
            if (projectUpdateViewModel == null)
            {
                ModelState.AddModelError("", "This data is not exist");
                return RedirectToAction("Index");
            }

            try
            {
                if (projectUpdateViewModel.files != null && projectUpdateViewModel.ImageID != null)
                {
                    try
                    {
                        for (int i = 0; i < projectUpdateViewModel.ImageID.Count; i++)
                        {
                            var image = _unitOfWork.imageRepository.Find(x => x.Id == projectUpdateViewModel.ImageID[i]);
                            await projectUpdateViewModel.files[i].UpdateAsyc(_env, image, "project", _unitOfWork);
                        }
                    }
                    catch
                    {
                    }
                }
                else if (projectUpdateViewModel.files != null)
                {
                    var emptyImage = _unitOfWork.projectRepository.Find(x => x.Id == projectUpdateViewModel.Id);

                    var imagesid = await projectUpdateViewModel.files.SaveImageCollectionAsync(_env, "project", _unitOfWork);
                    foreach (var item in imagesid)
                    {
                        var resultData = new ProjectImage
                        {
                            ProjectId = emptyImage.Id,
                            ImageId = item
                        };
                        await _unitOfWork.projectImageRepository.AddAsync(resultData);
                    }
                }
                var resultProject = _projectFacade.Update(projectUpdateViewModel);
                if (resultProject)
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _unitOfWork.Rollback();
                    }
                }
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        #endregion UPDATE

        #region DELETE

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Index");
            }
            var isresult = _projectFacade.Delete(id);

            if (isresult)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        #endregion DELETE

        #region ::PRIVITE::

        private async Task<IActionResult> AllSave(RESULT<Project> resultProject, List<int> resultimage)
        {
            if (resultimage.Count > 0 && resultProject.IsDone)
            {
                await AddProjectImageAddViewModel(resultProject, resultimage);
                if (await _unitOfWork.CommitAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Rollback();
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        private async Task AddProjectImageAddViewModel(RESULT<Project> resultProject, List<int> resultimage)
        {
            try
            {
                foreach (var item in resultimage)
                {
                    ProjectImageAddViewModel projectImageAddViewModel = new ProjectImageAddViewModel
                    {
                        ImageId = item,
                        ProjectId = resultProject.Data.Id
                    };
                    await _projectImageFacade.Add(projectImageAddViewModel);
                }
            }
            catch 
            {

                
            }
        }

        #endregion ::PRIVITE::
    }
}