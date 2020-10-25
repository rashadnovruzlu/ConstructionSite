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
            var resultGaleryViewModel = new Galery
            {
                Id = galeryAddViewModel.Id,
                TitleAz = galeryAddViewModel.TitleAz,
                TitleEn = galeryAddViewModel.TitleEn,
                TitleRu = galeryAddViewModel.TitleRu
            };
            var resultGalery = await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return resultGalery;
        }

        #endregion ::ADD::

        #region ::DELETE::

        public async Task<RESULT<Galery>> Delete(int id)
        {
            var resultGaleryFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            return await _unitOfWork.GaleryRepstory.DeleteAsync(resultGaleryFind);

        }

        #endregion ::DELETE::

        #region ::UPDATE::

        public async Task<RESULT<Galery>> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            var resultGaleryUpdateViewModelFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == galeryUpdateViewModel.Id);
            var resultGaleryUpdateViewModel = new Galery
            {
                Id = resultGaleryUpdateViewModelFind.Id,
                TitleAz = resultGaleryUpdateViewModelFind.TitleAz,
                TitleEn = resultGaleryUpdateViewModelFind.TitleEn,
                TitleRu = resultGaleryUpdateViewModelFind.TitleRu
            };
            return await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);

        }

        #endregion ::UPDATE::

        #region CECHEDTRANSACTION::




        public async Task<GaleryUpdateViewModel> FindUpdate(int id)
        {
            var result = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            GaleryUpdateViewModel galeryUpdateViewModel = new GaleryUpdateViewModel
            {
                Id = result.Id,
                TitleAz = result.TitleAz,
                TitleEn = result.TitleEn,
                TitleRu = result.TitleRu
            };
            return await Task.FromResult(galeryUpdateViewModel);
        }

        #endregion CECHEDTRANSACTION::
    }
}