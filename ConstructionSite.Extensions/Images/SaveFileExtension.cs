using ConstructionSite.Entity.Models;
using ConstructionSite.Extensions.Paths;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.IO;
using System.Threading.Tasks;

namespace ConstructionSite.Extensions.Images
{
    public static class SaveFileExtension
    {
        public async static Task<int> SaveImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            if (file.IsImage())
            { 
                
                string name = await file.SaveAsync(_env,subFolder);
                image.Title = name;
                image.Path = name;
                await _unitOfWork.imageRepository.AddAsync(image);
                _unitOfWork.Dispose();
            }
            return image.Id;
        }
        public static void DeleteImage(this IFormFile file, IWebHostEnvironment _env, string subFolder, Image image, IUnitOfWork _unitOfWork)
        {
            string fileName= file.GetFileName();
            string pathPhoto=Path.Combine(_env.WebRootPath,"images", subFolder);
            if (File.Exists(pathPhoto))
            {
              var result=  _unitOfWork.imageRepository.Find(x=>x.Title==fileName);
                if (result!=null)
                {
                var   imageresult=  _unitOfWork.imageRepository.Delete(result);
                    if (imageresult.IsDone)
                    {
                        var subResult = Path.Combine(pathPhoto,fileName);
                        File.Delete(subResult);
                    }
                   
                }
              
            }

           
        }
    }
}