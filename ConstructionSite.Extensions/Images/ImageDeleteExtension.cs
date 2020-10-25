using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Core;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageDeleteExtension
    {
        private const string _IMAGE = "images";

        public static bool Delete(this IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult = _unitOfWork.imageRepository.GetById(imageId);

            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result = _unitOfWork.imageRepository.Delete(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            _unitOfWork.Commit();
            return result.IsDone;
        }
       

        public async static Task<bool> DeleteAsyc(this IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult = await _unitOfWork.imageRepository.GetByIdAsync(imageId);

            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result = await _unitOfWork.imageRepository.DeleteAsync(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }

        public async static Task<bool> UpdateAsyc(this IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult = await _unitOfWork.imageRepository.GetByIdAsync(imageId);

            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result = await _unitOfWork.imageRepository.DeleteAsync(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }

        public async static Task<bool> UpdateAsyc(ICollection<IFormFile> formFiles, int imageID, IWebHostEnvironment _env, string subFolder, IUnitOfWork _unitOfWork)
        {
            bool isSuccess = false;
            foreach (var item in formFiles)
            {
                var resultImage = await _unitOfWork.imageRepository.FindAsync(x => x.Id == imageID);
                var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, resultImage.Title, _IMAGE);
                Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
                var imageSuccess = await item.SaveImageAsync(_env, subFolder, resultImage, _unitOfWork);
                var imageDbSuccess = await _unitOfWork.imageRepository.UpdateAsync(resultImage);
                if (imageSuccess && imageDbSuccess.IsDone)
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        isSuccess = true;
                    }
                    isSuccess = false;
                }
                else
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }
    }
}