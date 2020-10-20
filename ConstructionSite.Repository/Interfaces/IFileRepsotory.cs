using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Interfaces
{
    public interface IFileRepsotory
    {
        Task<bool> AddAsync(IFormFile formFile);
        Task<bool> AddRangeAsync(List<IFormFile> formFiles);

    }
}
