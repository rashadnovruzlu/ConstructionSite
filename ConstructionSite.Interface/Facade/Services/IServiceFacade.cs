using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Helpers.Core;
using back = ConstructionSite.ViwModel.AdminViewModels.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Servics
{
    public interface IServiceFacade
    {
        Task<RESULT<ConstructionSite.Entity.Models.Service>> Add(ServiceAddViewModel serviceAddViewModel);
        Task<ConstructionSite.DTO.FrontViewModels.Service.ServiceDeatilyViewModel> GetDeaiy(int id, string _lang);

        Task<bool> Update();
    }
}
