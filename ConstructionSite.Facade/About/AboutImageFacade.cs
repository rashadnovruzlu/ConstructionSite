using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.About;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.About;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.About
{
    public class AboutImageFacade : IAboutImageFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public AboutImageFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RESULT<AboutImage>> AddAsync(AboutImageAddViewModel aboutImageAddViewModel)
        {
            var resultAboutImageAddViewModel = await aboutImageAddViewModel.MappedAsync<AboutImage>();
            return await _unitOfWork.AboutImageRepository.AddAsync(resultAboutImageAddViewModel);
        }

        public async Task<RESULT<AboutImage>> Update(AboutImageUpdateViewModel aboutImageUpdateViewModel)
        {
            var result = await _unitOfWork.AboutImageRepository.FindAsync(x => x.Id == aboutImageUpdateViewModel.Id);
            var resultAbout = await _unitOfWork.AboutRepository.FindAsync(x => x.Id == aboutImageUpdateViewModel.Id);

            return await _unitOfWork.AboutImageRepository.UpdateAsync(result);
        }
    }
}
