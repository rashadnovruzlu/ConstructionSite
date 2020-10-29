using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.About;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.About
{
    public interface IAboutImageFacade
    {
        Task<RESULT<AboutImage>> AddAsync(AboutImageAddViewModel aboutImageAddViewModel);

        Task<RESULT<AboutImage>> Update(AboutImageUpdateViewModel aboutImageUpdateViewModel);
    }
}