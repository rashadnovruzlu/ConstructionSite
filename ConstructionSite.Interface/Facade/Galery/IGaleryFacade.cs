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

        Task<RESULT<name.Galery>> Delete(int id);
        Task<GaleryUpdateViewModel> FindUpdate(int id);
        Task<RESULT<name.Galery>> Update(GaleryUpdateViewModel galeryUpdateViewModel);
        Task<GaleryViewModel> GetAll();
    }
}
