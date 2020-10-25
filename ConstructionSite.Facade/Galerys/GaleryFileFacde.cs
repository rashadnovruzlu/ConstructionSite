using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Galery;
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

        public async Task<RESULT<GaleryFile>> Add(GaleryFileAddViewModel galeryFileAddViewModel)
        {
            var resultGaleryFileAddViewModel = await galeryFileAddViewModel.MappedAsync<GaleryFile>();
            return await _unitOfWork.GaleryFileRepstory.AddAsync(resultGaleryFileAddViewModel);

        }

        #endregion ::ADD::

        #region ::DELETE::

        public async Task<RESULT<GaleryFile>> Delete(int id)
        {
            var resultgaleryFileViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == id);
            var resultgaerlyFileViewModelMapping = await resultgaleryFileViewModel.MappedAsync<GaleryFile>();
            return await _unitOfWork.GaleryFileRepstory.DeleteAsync(resultgaerlyFileViewModelMapping);

        }

        #endregion ::DELETE::

        #region ::UPDATE::

        public async Task<RESULT<GaleryFile>> Update(GaleryFileUpdateViewModel galeryFileUpdateViewModel)
        {
            var resultGaleryFileUpdateViewModel = await _unitOfWork.GaleryFileRepstory.FindAsync(x => x.Id == galeryFileUpdateViewModel.Id);
            return await _unitOfWork.GaleryFileRepstory.UpdateAsync(resultGaleryFileUpdateViewModel);

        }

        #endregion ::UPDATE::

        #region ::GETALL::

        public async Task<IQueryable<GaleryFileViewModel>> GetAll(string _lang)
        {
            var result = _unitOfWork.GaleryFileRepstory
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
                    });
            return await Task.FromResult(result);

        }

        #endregion ::GETALL::




    }
}