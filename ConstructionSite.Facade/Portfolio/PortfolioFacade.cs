using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
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
            var resultPortfolioAddModel = new Entity.Models.Portfolio
            {
                NameAz = portfolioAddModel.NameAz,
                NameEn = portfolioAddModel.NameEn,
                NameRu = portfolioAddModel.NameRu,

            };
            var resunltPortfoliAdd = await _unitOfWork.portfolioRepository.AddAsync(resultPortfolioAddModel);
            return resunltPortfoliAdd;
        }

        public async Task<bool> Delete(int id)
        {
            var resultPortfoli = await _unitOfWork.portfolioRepository.FindAsync(x => x.Id == id);
            var result = await _unitOfWork.portfolioRepository.DeleteAsync(resultPortfoli);
            return result.IsDone;
        }

        public List<PortfolioViewModel> GetAll(string _lang)
        {
            var resultPortfolio = _unitOfWork.portfolioRepository.GetAll()
                   .Select(x => new PortfolioViewModel
                   {
                       Id = x.Id,
                       ImagePath = x.PortfolioImages.Select(x => x.Image.Path).FirstOrDefault(),
                       Name = x.FindName(_lang)
                   })
                   .ToList();
            return resultPortfolio;
        }

        // public async Task<RESULT<Entity.Models.Portfolio>> Update(Entity.Models.Portfolio)
    }
}