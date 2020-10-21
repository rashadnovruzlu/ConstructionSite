using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Galerys
{
    public class GaleryFileFacde : IGaleryFileFacde
    {
        private readonly IUnitOfWork _unitOfWork;
        public GaleryFileFacde(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Add(GaleryFileAddViewModel galeryFileAddViewModel)
        {
            var resultGaleryFileAddViewModel = await galeryFileAddViewModel.MappedAsync<GaleryFile>();
            await _unitOfWork.GaleryFileRepstory.AddAsync(resultGaleryFileAddViewModel);
            return await CeckedTransaction();
        }

        public async Task<bool> Delete(int id)
        {
            var resultgaleryFileViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == id);
            var resultgaerlyFileViewModelMapping = await resultgaleryFileViewModel.MappedAsync<GaleryFile>();
            await _unitOfWork.GaleryFileRepstory.DeleteAsync(resultgaerlyFileViewModelMapping);
            return await CeckedTransaction();
        }

        public async Task<bool> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel)
        {
            var resultGaleryFileUpdateViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == galeryFileUpdateViewModel.Id);
            await _unitOfWork.GaleryFileRepstory.UpdateAsync(resultGaleryFileUpdateViewModel);
            return await CeckedTransaction();
        }







        #region CECHEDTRANSACTION::
        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }
        #endregion
    }
}
