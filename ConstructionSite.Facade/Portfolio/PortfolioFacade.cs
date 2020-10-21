using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Portfolio;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Portfolio
{
    public class PortfolioImageFacade : IPortfolioImageFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public PortfolioImageFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region ::ADD::
        public async Task<bool> Add(PortfolioImageAddViewModel portfolioImageAddViewModel)
        {
            var resultportfolioImageAddViewModel = await portfolioImageAddViewModel.MappedAsync<PortfolioImage>();
            await _unitOfWork.PortfolioImageRepostory.AddAsync(resultportfolioImageAddViewModel);
            return await CeckedTransaction();
        }


        #endregion

        #region ::DELTE::
        public async Task<bool> Delete(int id)
        {
            var resultPortfolioImage = await _unitOfWork.PortfolioImageRepostory.FindAllAsync(x => x.Id == id);
            var resultPortfolioImageMapping = await resultPortfolioImage.MappedAsync<PortfolioImage>();
            await _unitOfWork.PortfolioImageRepostory.DeleteAsync(resultPortfolioImageMapping);
            return await CeckedTransaction();

        }

        #endregion

        #region ::UPDATE::
        public async Task<bool> Update(PortfolioImageUpdateViewModel portfolioImageUpdateViewModel)
        {
            var resultportfolioImageUpdateViewModel = await _unitOfWork.PortfolioImageRepostory.FindAllAsync(x => x.Id == portfolioImageUpdateViewModel.Id);
            var resultportfolioImageUpdateViewModelMapped = await resultportfolioImageUpdateViewModel.MappedAsync<PortfolioImage>();
            await _unitOfWork.PortfolioImageRepostory.UpdateAsync(resultportfolioImageUpdateViewModelMapped);
            return await CeckedTransaction();
        }
        #endregion

        #region ::GETALL::

        #endregion


        #region CECHEDTRANSACTION::

        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }

        #endregion CECHEDTRANSACTION::
    }
}
