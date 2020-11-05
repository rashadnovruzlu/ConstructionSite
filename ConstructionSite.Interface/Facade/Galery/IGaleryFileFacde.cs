using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using ConstructionSite.ViwModel.FrontViewModels.Galery;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Galery
{
    public interface IGaleryFileFacde
    {
        Task<RESULT<GaleryFile>> Add(GaleryFileAddViewModel galeryFileAddViewModel);

        Task<RESULT<GaleryFile>> Delete(int id);

        Task<RESULT<GaleryFile>> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel);

        List<GaleryVidoViewoModel> GetAllVideo(string _lang);

        List<GaleryImageViewModel> GetAllImage(string _lang);
    }
}