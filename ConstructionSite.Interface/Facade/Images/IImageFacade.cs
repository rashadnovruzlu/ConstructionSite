using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Images
{
    public interface IImageFacade
    {
        Task<RESULT<Image>> Update(Image image);
    }
}