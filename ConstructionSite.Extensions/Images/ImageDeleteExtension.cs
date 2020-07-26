using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Core;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
   public static  class ImageDeleteExtension
    {
        private const string _IMAGE = "images";
        public static bool Delete(this IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult = _unitOfWork.imageRepository.GetById(imageId);


            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result = _unitOfWork.imageRepository.Delete(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }
        public async static Task<bool> DeleteAsyc(this IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult =await _unitOfWork.imageRepository.GetByIdAsync(imageId);


            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result =await _unitOfWork.imageRepository.DeleteAsync(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }
    }
}
