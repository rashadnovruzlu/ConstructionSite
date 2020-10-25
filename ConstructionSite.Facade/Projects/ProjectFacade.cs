using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
