using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Interfaces.Facade;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Galerys
{
    public class GaleryFileFacde : IGaleryFileFacde
    {
        #region ::FILDS::
        private readonly IUnitOfWork _unitOfWork;
        #endregion


        public GaleryFileFacde(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ::ADD::

        public async Task<bool> Add(GaleryFileAddViewModel galeryFileAddViewModel)
        {
            var resultGaleryFileAddViewModel = await galeryFileAddViewModel.MappedAsync<GaleryFile>();
            await _unitOfWork.GaleryFileRepstory.AddAsync(resultGaleryFileAddViewModel);
            return await CeckedTransaction();
        }

        #endregion ::ADD::

        #region ::DELETE::

        public async Task<bool> Delete(int id)
        {
            var resultgaleryFileViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == id);
            var resultgaerlyFileViewModelMapping = await resultgaleryFileViewModel.MappedAsync<GaleryFile>();
            await _unitOfWork.GaleryFileRepstory.DeleteAsync(resultgaerlyFileViewModelMapping);
            return await CeckedTransaction();
        }

        #endregion ::DELETE::

        #region ::UPDATE::

        public async Task<bool> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel)
        {
            var resultGaleryFileUpdateViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == galeryFileUpdateViewModel.Id);
            await _unitOfWork.GaleryFileRepstory.UpdateAsync(resultGaleryFileUpdateViewModel);
            return await CeckedTransaction();
        }

        #endregion ::UPDATE::

        #region ::GETALL::

        public async Task<List<GaleryFileViewModel>> GetAll(string _lang)
        {
            return await _unitOfWork.GaleryFileRepstory
                   .GetAll()
                   .Include(x => x.Image)
                   .Include(x => x.Galery)
                   .Select(x => new GaleryFileViewModel
                   {
                       Id = x.Id,
                       GaleryId = x.GaleryId,
                       ImageId = x.ImageId,
                       Path = x.Image.Path,
                       Title = x.Image.Title,

                       VideoPath = x.Image.VideoPath,
                       GaleryTitle = x.Galery.FindTitle(_lang)
                   })
                   .ToListAsync();
        }

        #endregion ::GETALL::

        #region CECHEDTRANSACTION::

        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }

        #endregion CECHEDTRANSACTION::
    }
}