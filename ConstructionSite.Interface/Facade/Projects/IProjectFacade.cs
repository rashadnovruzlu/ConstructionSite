using ConstructionSite.DTO.AdminViewModels.Project;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Projects
{
    public interface IProjectFacade
    {
        Task<RESULT<Project>> Add(Project project);
        public List<ProjectViewModel> GetAll(string _lang);
    }
}