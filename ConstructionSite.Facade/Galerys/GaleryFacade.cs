using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.Repository.Interfaces;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Galerys

{
    public class GaleryFacade : IGaleryFacade
    {

        #region ::FILEDS::
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region ::CTOR::
        public GaleryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region ::GETALL::
        public async Task<GaleryViewModel> GetAll()
        {
            return await _unitOfWork.GaleryRepstory.GetAll()
                  .MappedAsync<GaleryViewModel>
                  ();


        }
        #endregion

        #region ::ADD::
        public async Task<bool> Add(GaleryAddViewModel galeryAddViewModel)
        {
            var resultGaleryViewModel = await galeryAddViewModel.MappedAsync<Galery>();
            await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return await CeckedTransaction();

        }
        #endregion

        #region ::DELETE::
        public async Task<bool> Delete(int id)
        {
            var resultGaleryFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            await _unitOfWork.GaleryRepstory.DeleteAsync(resultGaleryFind);
            return await CeckedTransaction();
        }
        #endregion

        #region ::UPDATE::
        public async Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            var resultGaleryUpdateViewModel = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == galeryUpdateViewModel.Id);
            await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);
            return await CeckedTransaction();
        }
        #endregion

        #region CECHEDTRANSACTION::
        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }
        #endregion





    }
}
