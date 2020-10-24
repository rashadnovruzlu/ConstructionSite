using ConstructionSite.Helpers.Core;
using port =ConstructionSite.Entity.Models;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.Portfolio;

namespace ConstructionSite.Interface.Facade.Portfolio
{
   public interface IPortfolioFacade
    {
        Task<RESULT<port.Portfolio>> Add(PortfolioAddModel portfolioAddModel);
        Task<bool> Delete(int id);
       
    }
}
