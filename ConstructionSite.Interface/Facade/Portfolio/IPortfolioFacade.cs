using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Helpers.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using port = ConstructionSite.Entity.Models;

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