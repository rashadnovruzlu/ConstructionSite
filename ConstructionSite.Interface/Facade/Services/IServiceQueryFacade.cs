using ConstructionSite.DTO.AdminViewModels.Service;
using ConstructionSite.Helpers.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Services
{
    public interface IServiceQueryFacade
    {
        Task<List<ServiceViewModel>> GetAll(string _lang);
    }
}
