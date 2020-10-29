using ConstructionSite.DTO.FrontViewModels.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Services
{
    public interface IServiceQueryFacade
    {
        Task<List<ServiceViewModel>> GetAll(string _lang);
    }
}