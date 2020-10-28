using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Project;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Projects
{
    public interface IProjectImageFacade
    {
        Task<RESULT<ProjectImage>> Add(ProjectImageAddViewModel projectImage);
    }
}