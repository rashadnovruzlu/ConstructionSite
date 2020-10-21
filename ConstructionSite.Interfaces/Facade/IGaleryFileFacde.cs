using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Facade
{
    public interface IGaleryFileFacde
    {
        Task<bool> Add(GaleryFileAddViewModel galeryFileAddViewModel);
        Task<bool> Delete(int id);
        Task<bool> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel);

       
    }
}
