using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Servics
{
    public interface IServiceFacade
    {
        Task<RESULT<ConstructionSite.Entity.Models.Service>> Add(ServiceAddViewModel serviceAddViewModel);

        Task<bool> Update();
    }
}
