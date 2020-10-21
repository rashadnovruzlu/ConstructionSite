using ConstructionSite.ViwModel.AdminViewModels.Portfolio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Facade
{
    public interface IPortfolioImageFacade
    {
        Task<bool> Add(PortfolioImageAddViewModel portfolioImageAddViewModel);
        Task<bool> Delete(int id);
        Task<bool> Update(PortfolioImageUpdateViewModel portfolioImageUpdateViewModel);
        Task<bool> GetAll();
    }
}
