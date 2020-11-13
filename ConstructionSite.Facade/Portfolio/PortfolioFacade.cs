using ConstructionSite.DTO.AdminViewModels.Portfolio;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Portfolio;
using ConstructionSite.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = ConstructionSite.Entity.Models;

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

        public async Task<RESULT<data.Portfolio>> Update(PortfoliUpdateViewModel portfoliUpdateViewModel)
        {
            var resultPortfoliUpdateViewModel = await _unitOfWork.portfolioRepository.FindAsync(x => x.Id == portfoliUpdateViewModel.id);
            resultPortfoliUpdateViewModel.NameAz = portfoliUpdateViewModel.NameAz;
            resultPortfoliUpdateViewModel.NameEn = portfoliUpdateViewModel.NameEn;
            resultPortfoliUpdateViewModel.NameRu = portfoliUpdateViewModel.NameRu;
            return await _unitOfWork.portfolioRepository.UpdateAsync(resultPortfoliUpdateViewModel);
        }

        public async Task<bool> Delete(int id)
        {
            var resultPortfoli = await _unitOfWork.portfolioRepository.FindAsync(x => x.Id == id);
            var result = await _unitOfWork.portfolioRepository.DeleteAsync(resultPortfoli);
            return result.IsDone;
        }

        public PortfoliUpdateViewModel GetForUpdate(int id)
        {
            var resultPortfoliUpdateViewModel = _unitOfWork.portfolioRepository.GetAll()
                .Select(x => new PortfoliUpdateViewModel
                {
                    id = x.Id,
                    NameAz = x.NameAz,
                    NameEn = x.NameEn,
                    NameRu = x.NameRu,
                    Images = x.PortfolioImages.Select(x => x.Image).ToList()
                })
                .SingleOrDefault(x => x.id == id);
            return resultPortfoliUpdateViewModel;
        }

        public List<PortfolioViewModel> GetAll(string _lang)
        {
            var resultPortfolio = _unitOfWork.portfolioRepository.GetAll()
                   .Select(x => new PortfolioViewModel
                   {
                       Id = x.Id,
                       Name = x.FindName(_lang)
                   })
                   .OrderByDescending(x => x.Id)
                   .ToList();
            return resultPortfolio;
        }

        // public async Task<RESULT<Entity.Models.Portfolio>> Update(Entity.Models.Portfolio)
    }
}