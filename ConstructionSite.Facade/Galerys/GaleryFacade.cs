using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
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

        public async Task<bool> Add(GaleryAddViewModel galeryAddViewModel)
        {
            var resultGaleryViewModel = await galeryAddViewModel.MappedAsync<Galery>();
            await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return await CeckedTransaction();
        }

        #endregion ::ADD::

        #region ::DELETE::

        public async Task<bool> Delete(int id)
        {
            var resultGaleryFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            await _unitOfWork.GaleryRepstory.DeleteAsync(resultGaleryFind);
            return await CeckedTransaction();
        }

        #endregion ::DELETE::

        #region ::UPDATE::

        public async Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            var resultGaleryUpdateViewModel = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == galeryUpdateViewModel.Id);
            await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);
            return await CeckedTransaction();
        }

        #endregion ::UPDATE::

        #region CECHEDTRANSACTION::

        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }

        #endregion CECHEDTRANSACTION::
    }
}