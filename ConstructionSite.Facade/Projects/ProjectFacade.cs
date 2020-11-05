using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Projects
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public ProjectFacade(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<RESULT<Project>> Add(Project project)
        {
            return await _unitOfWork.projectRepository.AddAsync(project);
        }

        public List<ProjectViewModel> GetAll(string _lang)
        {
            var resultProject = _unitOfWork.projectRepository.GetAll()
                  .Select(x => new ProjectViewModel
                  {
                      Id = x.Id,
                      Content = x.FindContent(_lang),
                      Image = x.ProjectImages.Select(x => x.Image.Path).FirstOrDefault(),
                      Name = x.FindName(_lang),
                      ImageId = x.ProjectImages.Select(x => x.ImageId).FirstOrDefault(),
                      PortfolioID = x.PortfolioId
                  })
                  .OrderByDescending(x=>x.Id)
                  .ToList();
            return resultProject;
        }

        public ProjectUpdateViewModel GetForUpdate(int id)
        {
            var result = _unitOfWork.projectRepository
                .GetAll()
                .Select(x => new ProjectUpdateViewModel
                {
                    Id = x.Id,
                    ContentAz = x.ContentAz,
                    ContentEn = x.ContentEn,
                    ContentRu = x.ContentRu,
                    NameAz = x.NameAz,
                    NameEn = x.NameEn,
                    NameRu = x.NameRu,
                    PortfolioId = x.PortfolioId,
                    ImageID = x.ProjectImages.Select(x => x.ImageId).ToList(),
                    Images = x.ProjectImages.Select(x => x.Image).ToList()
                })
                .SingleOrDefault(x => x.Id == id);
            return result;
        }

        public bool Update(ProjectUpdateViewModel projectUpdateViewModel)
        {
            var resultprojectUpdateViewModel = _unitOfWork.projectRepository.Find(x => x.Id == projectUpdateViewModel.Id); ;
            resultprojectUpdateViewModel.NameAz = projectUpdateViewModel.NameAz;
            resultprojectUpdateViewModel.NameEn = projectUpdateViewModel.NameEn;
            resultprojectUpdateViewModel.NameRu = projectUpdateViewModel.NameRu;
            resultprojectUpdateViewModel.ContentAz = projectUpdateViewModel.ContentAz;
            resultprojectUpdateViewModel.ContentEn = projectUpdateViewModel.ContentEn;
            resultprojectUpdateViewModel.ContentRu = projectUpdateViewModel.ContentRu;
            resultprojectUpdateViewModel.PortfolioId = projectUpdateViewModel.PortfolioId;

            return _unitOfWork.projectRepository.Update(resultprojectUpdateViewModel).IsDone;
        }

        public bool Delete(int id)
        {
            var data = _unitOfWork.projectRepository.Find(x => x.Id == id);
            var imageId = _unitOfWork.projectImageRepository.GetAll()
                  .Where(x => x.ProjectId == data.Id)
                  .Select(x => x.ImageId).ToArray();
            _unitOfWork.projectRepository.Delete(data);
            return _env.Delete(imageId, "About", _unitOfWork);
        }
    }
}