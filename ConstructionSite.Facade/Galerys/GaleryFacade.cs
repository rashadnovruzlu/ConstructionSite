using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Galerys

{
    public class GaleryFacade : IGaleryFacade
    {
        /// <summary>
        /// PortfolioImage
        /// ServiceImage
        /// </summary>
        #region ::FILEDS::

        private readonly IUnitOfWork _unitOfWork;

        #endregion ::FILEDS::

        #region ::CTOR::

        public GaleryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion ::CTOR::

        #region ::GETALL::

        public async Task<GaleryViewModel> GetAll()
        {
            return await _unitOfWork.GaleryRepstory.GetAll()
                  .MappedAsync<GaleryViewModel>
                  ();
        }

        #endregion ::GETALL::

        #region ::ADD::

        public async Task<RESULT<Galery>> Add(GaleryAddViewModel galeryAddViewModel)
        {
            var resultGaleryViewModel = await galeryAddViewModel.MappedAsync<Galery>();
            var resultGalery = await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return resultGalery;
        }

        #endregion ::ADD::

        #region ::DELETE::

        public async Task<bool> Delete(int id)
        {
            var resultGaleryFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            await _unitOfWork.GaleryRepstory.DeleteAsync(resultGaleryFind);
            return await CeckedTransactionAsync();
        }

        #endregion ::DELETE::

        #region ::UPDATE::

        public async Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            var resultGaleryUpdateViewModel = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == galeryUpdateViewModel.Id);
            await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);
            return await CeckedTransactionAsync();
        }

        #endregion ::UPDATE::

        #region CECHEDTRANSACTION::

        private async Task<bool> CeckedTransactionAsync()
        {
            bool isResult = false;
            isResult = await _unitOfWork.CommitAsync() > 0;
            return isResult;
        }
        private bool CeckedTransaction()
        {
            try
            {
                return _unitOfWork.Commit() > 0;
            }
            catch (System.Exception ex)
            {
                var str = ex.InnerException;

                return false;
            }
        }

        public async Task<GaleryUpdateViewModel> FindUpdate(int id)
        {
            var result = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            return await result.MappedAsync<GaleryUpdateViewModel>();
        }

        #endregion CECHEDTRANSACTION::
    }
}