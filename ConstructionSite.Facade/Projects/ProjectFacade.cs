using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Projects
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                  }).ToList();
            return resultProject;
        }
    }
}