using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
        private const string _IMAGE = "images";

        public async static Task<bool> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            bool IsResult=false;


            if (file!=null)
            {
              
               string FileNameAfterReName  =reNameFileName(file);
               string filePath             = createfilePathSaveHardDisk(_env,subFolder,FileNameAfterReName);

              
                bool folderIsCreatedSuccess= createFolder(_env,subFolder);
                if (!folderIsCreatedSuccess)
                {
                    IsResult=false;
                }
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image.Title =FileNameAfterReName;
                image.Path =createFilePathSaveDataBase(subFolder,FileNameAfterReName);
                var imageSaveUpdate= await _unitOfWork.imageRepository.AddAsync(image);
                if (!imageSaveUpdate.IsDone)
                {
                    IsResult=false;
                }
                IsResult=true;
            }
            return IsResult;
        }
        public async static Task<bool> UpdateAsyc(this IFormFile file, IWebHostEnvironment _env,  Image image,string subFolder,  IUnitOfWork _unitOfWork)
        {
            var imageGetById = _unitOfWork.imageRepository.GetById(image.Id);
            string filePathForDeleteFromHardDisk = createfilePathSaveHardDisk(_env, subFolder, imageGetById.Title);
            string fileNameAfterReName   =getFileAndReName(file);
            var filePathSaveFromHardDisk = createfilePathSaveHardDisk(_env, subFolder, fileNameAfterReName);
            string filePathSaveDataBase  = createFilePathSaveDataBase(subFolder, fileNameAfterReName);
            bool IsResult = false;


          
          
            if (file != null)
            {
                deleteFileFormHardDisk(filePathForDeleteFromHardDisk);
               
                
                   
                    file.saveImageForDisk(filePathSaveFromHardDisk);
                    IsResult=true;
                
                if (IsResult)
                {
                   
                    imageGetById.Title    = fileNameAfterReName;
                    imageGetById.Path     =  filePathSaveDataBase;
                    var updateImageResult = await _unitOfWork.imageRepository.UpdateAsync(imageGetById);
                    if (updateImageResult.IsDone)
                    {
                        IsResult=true;
                    }
                }
               
            }
            return IsResult;
        }
        public static bool DeleteAsyc( IWebHostEnvironment _env, Image image, string subFolder, IUnitOfWork _unitOfWork)
        {
           
            bool isResult=false;
            var imageDeleteFormHardDisk= createfilePathSaveHardDisk(_env,subFolder,image.Title);
            var imageDbResult= _unitOfWork.imageRepository.Delete(image);
           
            if (imageDbResult.IsDone)
            {
                deleteFileFormHardDisk(imageDeleteFormHardDisk);
                isResult = imageDbResult.IsDone;
            }
            return isResult;
             
        }
        public static bool DeleteAsyc(IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
           Image imageResult= _unitOfWork.imageRepository.GetById(imageId);


            var imageDeleteFormHardDisk = createfilePathSaveHardDisk(_env, subFolder, imageResult.Title);
            var result=  _unitOfWork.imageRepository.Delete(imageResult);
            deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }

        private static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }

        private static bool createFolder(IWebHostEnvironment _env, string subFolder)
        {
           string folderPath= Path.Combine(_env.WebRootPath, _IMAGE, subFolder);
            bool isResult=false;
            if (!Directory.Exists(folderPath))
            {
                isResult=true;
                Directory.CreateDirectory(folderPath);
            }
            return isResult;
        }

        private static string getFileAndReName(this IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            string result = Guid.NewGuid().ToString() + extension;
            return result.ToString();
        }
        private static string reNameFileName(IFormFile file)
        {
            string fileNameExtension = Path.GetExtension(file.FileName);
            string FileNameAfterReName = Guid.NewGuid().ToString() + fileNameExtension;
            return FileNameAfterReName;
        }
        private static string createfilePathSaveHardDisk(IWebHostEnvironment _env,string subFolder, string FileNameAfterReName)
        {
            string filePath = Path.Combine(_env.WebRootPath, _IMAGE, subFolder, FileNameAfterReName);
            return filePath;
        }
        private static string createFilePathSaveDataBase(string subFolder,string FileNameAfterReName)
        {
            return "/" + Path.Combine(_IMAGE, subFolder, FileNameAfterReName);
        }
        private static void deleteFileFormHardDisk(string PathForDeleteFile)
        {
           
            if (File.Exists(PathForDeleteFile))
            {
                
                File.Delete(PathForDeleteFile);
            }
           
        }
        private async static void saveImageForDisk(this IFormFile file, string path)
        {

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}