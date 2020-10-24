using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using name = ConstructionSite.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Galery
{
    public interface IGaleryFacade
    {
        Task<RESULT<name.Galery>> Add(GaleryAddViewModel galeryAddViewModel);

        Task<bool> Delete(int id);
        Task<GaleryUpdateViewModel> FindUpdate(int id);
        Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel);
        Task<GaleryViewModel> GetAll();
    }
}
