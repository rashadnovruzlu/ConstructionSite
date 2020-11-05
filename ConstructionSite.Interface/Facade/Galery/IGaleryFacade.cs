using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System.Collections.Generic;
using System.Threading.Tasks;
using name = ConstructionSite.Entity.Models;

namespace ConstructionSite.Interface.Facade.Galery
{
    public interface IGaleryFacade
    {
        Task<RESULT<name.Galery>> Add(GaleryAddViewModel galeryAddViewModel);

        Task<RESULT<name.Galery>> Delete(int id);

        GaleryUpdateViewModel GetForUpdate(int id);

        Task<bool> GetAndUpdate(int id, string input);

        Task<RESULT<name.Galery>> Update(GaleryUpdateViewModel galeryUpdateViewModel);

        List<GaleryViewModel> GetAll(string _lang);
    }
}