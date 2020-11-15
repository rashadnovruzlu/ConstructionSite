using ConstructionSite.Extensions.Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Extensions.Images
{
    public static class SliderImageExtension
    {
        private const string _IMAGE = "images";
        public static void DeleteSlider(this IWebHostEnvironment _env, string FolderName, string pathImage)
        {
            string pathHardDisk = Paths.DeletePathSaveHardDisk(_env, FolderName, pathImage, _IMAGE);
            Files.deleteFileFormHardDisk(pathHardDisk);
        }
    }
}
