using ConstructionSite.Entity.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interfaces.Repstory
{
    public interface IFIleRepostory
    {
        Task<bool> SaveImageCollectionAsync(ICollection<IFormFile> files, string WebHostEnvironment, string subFolder, Image image);
    }
}
