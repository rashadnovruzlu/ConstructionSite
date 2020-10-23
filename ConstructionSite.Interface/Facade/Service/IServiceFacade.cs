using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Service
{
    public interface IServiceFacade
    {
        Task<bool> Add(ServiceAddViewModel serviceAddViewModel);
        Task<List<ServiceImageViewModel>> GetAll(string _lang);
        Task<bool> Update()
    }
}
