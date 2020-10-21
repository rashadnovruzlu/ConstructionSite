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
        //  private readonly IGaleryRepstory _galeryRepstory;
        private readonly IUnitOfWork _unitOfWork;
        public GaleryFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(GaleryViewModel galeryViewModel)
        {
            var resultGaleryViewModel = await galeryViewModel.MappedAsync<Galery>();
            await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return await CeckedTransaction();

        }

        public async Task<bool> Delete(int id)
        {
            var resultGaleryFind = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == id);
            await _unitOfWork.GaleryRepstory.DeleteAsync(resultGaleryFind);
            return await CeckedTransaction();
        }

        public async Task<bool> Update(GaleryUpdateViewModel galeryUpdateViewModel)
        {
            var resultGaleryUpdateViewModel = await _unitOfWork.GaleryRepstory.FindAsync(x => x.Id == galeryUpdateViewModel.Id);
            await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);
            return await CeckedTransaction();
        }

        private async Task<bool> CeckedTransaction()
        {
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
