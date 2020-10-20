﻿using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Core;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class ImageExtensions
    {
        private const string _IMAGE = "images";

        #region ::Save::
        public async static Task<bool> SaveImageCollectionAsync(this ICollection<IFormFile> files, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            bool IsResult = false;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        string FileNameAfterReName = Helper.reNameFileName(file);
                        string filePath = Paths
                             .createfilePathSaveHardDisk(_env, subFolder, FileNameAfterReName, _IMAGE);

                        bool folderIsCreatedSuccess = Folders.createFolder(_env, subFolder, _IMAGE);
                        if (!folderIsCreatedSuccess)
                        {
                            IsResult = false;
                        }
                        await using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        image.Title = FileNameAfterReName;
                        image.Path = Paths.createFilePathSaveDataBase(subFolder, FileNameAfterReName, _IMAGE);
                        var imageSaveFile = await _unitOfWork.imageRepository.AddAsync(image);
                        if (!imageSaveFile.IsDone)
                        {
                            IsResult = false;
                        }

                    }
                    IsResult = true;
                }
                IsResult = true;
            }
            return IsResult;
        }

        public async static Task<bool> SaveImageAsync(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            bool IsResult = false;

            if (file != null)
            {
                string FileNameAfterReName = Helper.reNameFileName(file);
                string filePath = Paths
                     .createfilePathSaveHardDisk(_env, subFolder, FileNameAfterReName, _IMAGE);

                bool folderIsCreatedSuccess = Folders.createFolder(_env, subFolder, _IMAGE);
                if (!folderIsCreatedSuccess)
                {
                    IsResult = false;
                }
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image.Title = FileNameAfterReName;
                image.Path = Paths.createFilePathSaveDataBase(subFolder, FileNameAfterReName, _IMAGE);
                var imageSaveFile = await _unitOfWork.imageRepository.AddAsync(image);
                if (!imageSaveFile.IsDone)
                {
                    IsResult = false;
                }
                IsResult = true;
            }
            return IsResult;
        }
        #endregion


        #region ::UPDATE::
        public async static Task<bool> UpdateAsyc(this IFormFile file, IWebHostEnvironment _env, Image image, string subFolder, IUnitOfWork _unitOfWork)
        {
            bool IsResult = false;
            var imageGetById = _unitOfWork.imageRepository.GetById(image.Id);

            string filePathForDeleteFromHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageGetById.Title, _IMAGE);

            string fileNameAfterReName = Helper.reNameFileName(file);

            var filePathSaveFromHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, fileNameAfterReName, _IMAGE);

            string filePathSaveDataBase = Paths.createFilePathSaveDataBase(subFolder, fileNameAfterReName, _IMAGE);

            if (file != null)
            {
                Files.deleteFileFormHardDisk(filePathForDeleteFromHardDisk);

                file.saveImageForDisk(filePathSaveFromHardDisk);
                IsResult = true;

                if (IsResult)
                {
                    imageGetById.Title = fileNameAfterReName;
                    imageGetById.Path = filePathSaveDataBase;
                    var updateImageResult = await _unitOfWork.imageRepository.UpdateAsync(imageGetById);
                    if (updateImageResult.IsDone)
                    {
                        IsResult = true;
                    }
                }
            }
            return IsResult;
        }
        //public async static Task<bool> UpdateAsyc(this IFormFile file, IWebHostEnvironment _env, ICollection<Image>
        //    image, string subFolder, IUnitOfWork _unitOfWork)
        //{
        //    bool IsResult = false;
        //    var imageGetById = _unitOfWork.imageRepository.GetById(image.Id);

        //    string filePathForDeleteFromHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageGetById.Title, _IMAGE);

        //    string fileNameAfterReName = Helper.reNameFileName(file);

        //    var filePathSaveFromHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, fileNameAfterReName, _IMAGE);

        //    string filePathSaveDataBase = Paths.createFilePathSaveDataBase(subFolder, fileNameAfterReName, _IMAGE);

        //    if (file != null)
        //    {
        //        Files.deleteFileFormHardDisk(filePathForDeleteFromHardDisk);

        //        file.saveImageForDisk(filePathSaveFromHardDisk);
        //        IsResult = true;

        //        if (IsResult)
        //        {
        //            imageGetById.Title = fileNameAfterReName;
        //            imageGetById.Path = filePathSaveDataBase;
        //            var updateImageResult = await _unitOfWork.imageRepository.UpdateAsync(imageGetById);
        //            if (updateImageResult.IsDone)
        //            {
        //                IsResult = true;
        //            }
        //        }
        //    }
        //    return IsResult;
        //}
        #endregion

        #region ::DELETE::
        public static bool DeleteAsyc(IWebHostEnvironment _env, Image image, string subFolder, IUnitOfWork _unitOfWork)
        {
            bool isResult = false;
            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, image.Title, _IMAGE);
            var imageDbResult = _unitOfWork.imageRepository.Delete(image);

            if (imageDbResult.IsDone)
            {
                Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
                isResult = imageDbResult.IsDone;
            }
            return isResult;
        }

        public static bool Delete(IWebHostEnvironment _env, int imageId, string subFolder, IUnitOfWork _unitOfWork)
        {
            Image imageResult = _unitOfWork.imageRepository.GetById(imageId);

            var imageDeleteFormHardDisk = Paths.createfilePathSaveHardDisk(_env, subFolder, imageResult.Title, _IMAGE);
            var result = _unitOfWork.imageRepository.Delete(imageResult);
            Files.deleteFileFormHardDisk(imageDeleteFormHardDisk);
            return result.IsDone;
        }

        #endregion


        #region ::FILETYPE::
        private static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png" ||
                   file.ContentType == "image/x-png" ||
                   file.ContentType == "image/gif";
        }
        #endregion



    }
}