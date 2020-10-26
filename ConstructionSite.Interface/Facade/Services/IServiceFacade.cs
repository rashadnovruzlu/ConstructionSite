using ConstructionSite.DTO.AdminViewModels.Service;
using front = ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Helpers.Core;
using data = ConstructionSite.Entity.Models;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Servics
{
    public interface IServiceFacade
    {
        Task<RESULT<ConstructionSite.Entity.Models.Service>> Add(ServiceAddViewModel serviceAddViewModel);
        Task<RESULT<front.ServiceDeatilyViewModel>> GetDeaiy(int id, string _lang);

        Task<RESULT<data.Service>> Update(ServiceUpdateViewModel serviceUpdateViewModel);
    }
}
