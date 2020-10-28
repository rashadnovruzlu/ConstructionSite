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

        public List<GaleryFileFrontViewModel> GetAll(string _lang)
        {
            var result = _unitOfWork.GaleryRepstory.GetAll()
                .Include(x => x.GaleryFiles)
                .Select(x => new GaleryFileFrontViewModel
                {
                    Id = x.Id,
                    GaleryTitle = x.FindTitle(_lang),
                    ImageId = x.GaleryFiles.Select(x => x.Image.Id).FirstOrDefault(),
                    Path = x.GaleryFiles.Select(x => x.Image.Path).ToArray(),
                    VideoPath = x.GaleryFiles.Select(x => x.Image.VideoPath).FirstOrDefault()
                    ,
                    Title = x.FindTitle(_lang)
                });
            return result.ToList();
        }

        #endregion ::GETALL::
    }
}