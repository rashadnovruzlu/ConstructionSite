using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using ConstructionSite.ViwModel.FrontViewModels.Galery;
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

        #endregion ::FILDS::

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

        public List<GaleryVidoViewoModel> GetAllVideo(string _lang)
        {
            var data = (from galery in _unitOfWork.GaleryRepstory.GetAll()
                        join galeryFiles in _unitOfWork.GaleryFileRepstory.GetAll() on galery.Id equals galeryFiles.GaleryId
                        join images in _unitOfWork.imageRepository.GetAll() on galeryFiles.ImageId equals images.Id
                        where images.VideoPath != null
                        select new GaleryVidoViewoModel()
                        {
                            Title = galery.FindTitle(_lang),
                            Vidopaths = images.VideoPath
                        }).ToList();
            return data;

        }
        public List<GaleryImageViewModel> GetAllImage(string _lang)
        {
            var data = (from galery in _unitOfWork.GaleryRepstory.GetAll()
                        join galeryFiles in _unitOfWork.GaleryFileRepstory.GetAll() on galery.Id equals galeryFiles.GaleryId
                        join images in _unitOfWork.imageRepository.GetAll() on galeryFiles.ImageId equals images.Id
                        where images.Path != null
                        select new GaleryImageViewModel()
                        {
                            Title = galery.FindTitle(_lang),
                            Imagepaths = images.Path
                        }).ToList();
            return data;
        }

        #endregion ::GETALL::
    }
}