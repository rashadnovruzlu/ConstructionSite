﻿using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using data = ConstructionSite.ViwModel.FrontViewModels.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConstructionSite.Facade.Slider
{
    public class SliderFacade : ISliderFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public SliderFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<SliderViewModel> GetAll(string _lang)
        {
            return _unitOfWork.SliderRepostory
                  .GetAll()
                  .Select(x => new SliderViewModel
                  {
                      Content = x.FindContent(_lang),
                      Tittle = x.FindTitle(_lang),
                      PathImage = ConvertToBase64Format(x.SliderImages.Select(x => x.Image.Path).FirstOrDefault())

                  })
                  .ToList();
        }

        #region base64

        public static string ConvertToBase64Format(string path)
        {
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                return base64ImageRepresentation;
            }
            catch (Exception e)
            {
                
                throw;
            }
          
        }

        #endregion
        public Task<RESULT<Sliders>> Add(SliderAddViewModel sliderAddViewModel)
        {

            var resultSliderAddViewModel = new Sliders
            {
                TittleAz = sliderAddViewModel.TittleAz,
                TittleEn = sliderAddViewModel.TittleEn,
                TittleRu = sliderAddViewModel.TittleRu,
                ContentAz = sliderAddViewModel.ContentAz,
                ContentEn = sliderAddViewModel.ContentEn,
                ContentRu = sliderAddViewModel.ContentRu,


            };
            var result = _unitOfWork.SliderRepostory.AddAsync(resultSliderAddViewModel);
            return result;

        }
        public SliderUpdateViewModel GetUpdate(int id)
        {
            var resultSliderUpdateViewModel = _unitOfWork.SliderRepostory.GetAll()
               .Select(x => new SliderUpdateViewModel
               {
                   Id = x.Id,
                   TittleAz = x.TittleAz,
                   TittleEn = x.TittleEn,
                   TittleRu = x.TittleRu,
                   ContentAz = x.ContentAz,
                   ContentEn = x.ContentEn,
                   ContentRu = x.ContentRu
               })
               .SingleOrDefault(x => x.Id == id);
            return resultSliderUpdateViewModel;


        }
        public async Task<RESULT<Sliders>> Update(SliderUpdateViewModel sliderUpdateViewModel)
        {
            var resultsliderUpdateViewModel = new Sliders
            {
                Id = sliderUpdateViewModel.Id,
                TittleAz = sliderUpdateViewModel.TittleAz,
                TittleEn = sliderUpdateViewModel.TittleEn,
                TittleRu = sliderUpdateViewModel.TittleRu,
                ContentAz = sliderUpdateViewModel.ContentAz,
                ContentEn = sliderUpdateViewModel.ContentEn,
                ContentRu = sliderUpdateViewModel.ContentRu,

            };
            return await _unitOfWork.SliderRepostory.UpdateAsync(resultsliderUpdateViewModel);
        }
        public bool Delete(int id)
        {
            var result = _unitOfWork.SliderRepostory.Find(x => x.Id == id);
            _unitOfWork.SliderRepostory.Delete(result);
            return _unitOfWork.Commit() > 0;
        }

      

        public List<data.SliderViewModel> GetForSlider(string _lang)
        {
            return _unitOfWork.SliderRepostory
                  .GetAll()
                  .Select(x => new data.SliderViewModel
                  {
                      Content = x.FindContent(_lang),
                      Tittle = x.FindTitle(_lang),
                      imagePath = x.SliderImages.Select(x => x.Image.Path).FirstOrDefault()

                  })
                  .ToList();
           // return  _unitOfWork.SliderRepostory.UpdateAsync(resultsliderUpdateViewModel);
        }
    }
}
