using ConstructionSite.DTO.AdminViewModels.SubService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Services
{
    public interface ISubServiceFacade
    {
        List<SubServiceViewModel> GetAll(string _lang);
        Task<bool> Add(SubServiceAddModel subServiceAddModel);
        

    }
}
