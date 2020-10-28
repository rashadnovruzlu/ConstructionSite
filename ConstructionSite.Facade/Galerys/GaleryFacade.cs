﻿using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Mapping;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Galery;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Galery;
using System.Collections.Generic;
using System.Linq;
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
        public List<GaleryViewModel> GetAll(string _lang)
        {
            var resultGalery = _unitOfWork.GaleryRepstory.GetAll()
                  .Select(x => new GaleryViewModel
                  {
                      Id = x.Id,
                      Title = x.FindTitle(_lang),
                      Imagepath = x.GaleryFiles.Select(x => x.Image.Path).FirstOrDefault()
                  })
                  .ToList();

            return resultGalery;



        }
        public async Task<RESULT<Galery>> Add(GaleryAddViewModel galeryAddViewModel)
        {
            var resultGaleryViewModel = new Galery
            {
                Id = galeryAddViewModel.Id,
                TitleAz = galeryAddViewModel.TitleAz,
                TitleEn = galeryAddViewModel.TitleEn,
                TitleRu = galeryAddViewModel.TitleRu
            };
            var resultGaleryUpdate = await _unitOfWork.GaleryRepstory.AddAsync(resultGaleryViewModel);
            return resultGaleryUpdate;
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
               
                TitleAz = resultGaleryUpdateViewModelFind.TitleAz,
                TitleEn = resultGaleryUpdateViewModelFind.TitleEn,
                TitleRu = resultGaleryUpdateViewModelFind.TitleRu
            };
            return await _unitOfWork.GaleryRepstory.UpdateAsync(resultGaleryUpdateViewModel);

        }

        #endregion ::UPDATE::

        #region CECHEDTRANSACTION::




        public GaleryUpdateViewModel GetForUpdate(int id)
        {
            var resultGaleryUpdate = _unitOfWork.GaleryRepstory.GetAll()
                .Select(x => new GaleryUpdateViewModel
                {
                    Id = x.Id,
                    TitleAz = x.TitleAz,
                    TitleEn = x.TitleEn,
                    TitleRu = x.TitleRu,
                    Images = x.GaleryFiles.Select(x => x.Image).ToList()
                })
                .SingleOrDefault(x => x.Id == id);
            return resultGaleryUpdate;
         
        }

        #endregion CECHEDTRANSACTION::
    }
}