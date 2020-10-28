using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Repository.Abstract;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Portfolio
{
    public class PortfolioFacade : IPortfolioFacade
    {
        private IUnitOfWork _unitOfWork;

        public PortfolioFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT<Entity.Models.Portfolio>> Add(PortfolioAddModel portfolioAddModel)
        {
            var resultPortfolioAddModel = await portfolioAddModel.MappedAsync<Entity.Models.Portfolio>();
            var resunlt = await _unitOfWork.portfolioRepository.AddAsync(resultPortfolioAddModel);
            return resunlt;
        }

        public async Task<bool> Delete(int id)
        {
            var resultPortfoli = await _unitOfWork.portfolioRepository.FindAsync(x => x.Id == id);
            var result = await _unitOfWork.portfolioRepository.DeleteAsync(resultPortfoli);
            return result.IsDone;
        }

        // public async Task<RESULT<Entity.Models.Portfolio>> Update(Entity.Models.Portfolio)
    }
}