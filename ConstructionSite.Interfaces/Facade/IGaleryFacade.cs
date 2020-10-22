using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Facade
{
    public interface IGaleryFacade
    {
        Task<bool> Add(GaleryAddViewModel galeryAddViewModel);
        Task<bool> Delete(int id);
        Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel);
        Task<GaleryViewModel> GetAll();
    }
}
