using ConstructionSite.Helpers.Core;
using port = ConstructionSite.Entity.Models;
using System.Threading.Tasks;
using ConstructionSite.DTO.AdminViewModels.Portfolio;
using System.Collections.Generic;

namespace ConstructionSite.Interface.Facade.Portfolio
{
    public interface IPortfolioFacade
    {
        List<PortfolioViewModel> GetAll(string _lang);
        Task<RESULT<port.Portfolio>> Add(PortfolioAddModel portfolioAddModel);
        PortfoliUpdateViewModel GetForUpdate(int id);
        Task<RESULT<port.Portfolio>> Update(PortfoliUpdateViewModel portfoliUpdateViewModel);
        Task<bool> Delete(int id);

    }
}
