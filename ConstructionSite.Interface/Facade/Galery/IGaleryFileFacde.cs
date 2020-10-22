using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Galery
{
    public interface IGaleryFileFacde
    {
        Task<bool> Add(GaleryFileAddViewModel galeryFileAddViewModel);
        Task<bool> Delete(int id);
        Task<bool> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel);
        Task<List<GaleryFileViewModel>> GetAll(string _lang);



    }
}
