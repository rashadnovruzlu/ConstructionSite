using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.ViwModel.AdminViewModels.Portfolio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Portfolio
{
    public interface IPortfolioImageFacade
    {
        Task<RESULT<PortfolioImage>> Add(PortfolioImageAddViewModel portfolioImageAddViewModel);

        Task<RESULT<PortfolioImage>> Delete(int id);

        Task<RESULT<PortfolioImage>> Update(PortfolioImageUpdateViewModel portfolioImageUpdateViewModel);

        Task<List<PortfolioImageViewModel>> GetAll(string _lang);

    }
}