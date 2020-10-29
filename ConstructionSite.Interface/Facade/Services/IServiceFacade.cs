using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using back = ConstructionSite.DTO.AdminViewModels.Service;
using data = ConstructionSite.Entity.Models;
using front = ConstructionSite.DTO.FrontViewModels.Service;

namespace ConstructionSite.Interface.Facade.Servics
{
    public interface IServiceFacade
    {
        Task<List<back.ServiceViewModel>> GetAll(string _lang);
        Task<RESULT<ConstructionSite.Entity.Models.Service>> Add(ServiceAddViewModel serviceAddViewModel);

        Task<RESULT<front.ServiceDeatilyViewModel>> GetDeaiy(int id, string _lang);

        Task<RESULT<data.Service>> Update(ServiceUpdateViewModel serviceUpdateViewModel);

        bool Delete(int id);
    }
}