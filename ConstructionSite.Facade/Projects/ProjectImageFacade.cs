using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Projects;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Projects
{
    public class ProjectImageFacade : IProjectImageFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectImageFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT<ProjectImage>> Add(ProjectImageAddViewModel projectImageAddViewModel)
        {
            var resultprojectImageAddViewModel = await projectImageAddViewModel.MappedAsync<ProjectImage>();
            return await _unitOfWork.projectImageRepository.AddAsync(resultprojectImageAddViewModel);
        }
    }
}
