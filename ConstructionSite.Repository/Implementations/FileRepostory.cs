using ConstructionSite.Entity.Models;
using ConstructionSite.Interfaces.Repstory;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Implementations
{
    public class FileRepostory : IFIleRepostory
    {
        //public Task<bool> SaveImageCollectionAsync(ICollection<IFormFile> files, string WebHostEnvironment, string subFolder, Image image)
        //{
        //    files.SaveImageCollectionAsync
        //}
        public Task<bool> SaveImageCollectionAsync(ICollection<IFormFile> files, string WebHostEnvironment, string subFolder, Image image)
        {
            throw new NotImplementedException();
        }
    }
}
