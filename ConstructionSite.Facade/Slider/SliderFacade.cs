using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Images;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Slider;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Slider;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data = ConstructionSite.ViwModel.FrontViewModels.Slider;

namespace ConstructionSite.Facade.Slider
{
    public class SliderFacade : ISliderFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderFacade(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<SliderViewModel> GetBackAll(string _lang)
        {
            return _unitOfWork.SliderRepostory
                  .GetAll()
                  .Select(x => new SliderViewModel
                  {
                      Id = x.Id,
                      Content = x.FindContent(_lang),
                      Tittle = x.FindTitle(_lang),
                      PathImage = x.ImagePath
                  })
                  .ToList();
        }

        public List<ConstructionSite.ViwModel.FrontViewModels.Slider.SliderViewModel> GetAll(string _lang)
        {
            return _unitOfWork.SliderRepostory
                  .GetAll()
                  .Select(x => new ConstructionSite.ViwModel.FrontViewModels.Slider.SliderViewModel
                  {
                      Content = x.FindContent(_lang),
                      Tittle = x.FindTitle(_lang),
                      // PathImage = ConvertToBase64Format(x.SliderImages.Select(x => x.Image.Path).FirstOrDefault()),
                      imagePath = ConvertToBase64Format(x.ImagePath)
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
            catch
            {
            }
            return string.Empty;
        }

        #endregion base64

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
                ImagePath = sliderAddViewModel.ImagePath
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
                   ContentRu = x.ContentRu,
                   pathImage = x.ImagePath,
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
                ImagePath = sliderUpdateViewModel.pathImage
            };
            return await _unitOfWork.SliderRepostory.UpdateAsync(resultsliderUpdateViewModel);
        }

        public bool Delete(int id)
        {
            var result = _unitOfWork.SliderRepostory.Find(x => x.Id == id);
            _webHostEnvironment.DeleteFormHardDiskSlider("Slider", result.ImagePath);
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
                      imagePath = x.ImagePath
                  })
                  .ToList();
            // return  _unitOfWork.SliderRepostory.UpdateAsync(resultsliderUpdateViewModel);
        }
    }
}