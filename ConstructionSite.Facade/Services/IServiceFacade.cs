using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Services
{
    public interface IServiceFacade
    {
        Task<RESULT<Service>> Add(ServiceAddViewModel serviceAddViewModel);
        Task<List<ServiceImageViewModel>> GetAll(string _lang);
        Task<bool> Update();
    }
}
