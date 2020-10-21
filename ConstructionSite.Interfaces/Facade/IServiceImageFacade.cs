using ConstructionSite.ViwModel.AdminViewModels.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Facade
{
    public interface IServiceImageFacade
    {
        Task<List<ServiceImageViewModel>> GetAll(string _lang);
        Task<bool> Add(ServiceImageAddViewModel serviceImageAddViewModel);
        Task<bool> Delete(int id);
        Task<bool> Update(ServiceImageUpdateViewModel serviceImageUpdateViewModel);
    }
}
