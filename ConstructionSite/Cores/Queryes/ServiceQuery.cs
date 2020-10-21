using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstructionSite.Cores.Queryes
{
    public static class ServiceQuery
    {
        public static SingleService ServiceRepositoryQuery(this IUnitOfWork unitOfWork, int id, string lang)
        {
          return  _unitOfWork.ServiceRepository
                       .GetAll()
                       .Include(x => x.ServiceImages)
                       .Select(x => new SingleService
                       {
                           Id = x.Id,
                           Name = x.FindName(_lang),
                           Title = x.FindTitle(_lang),
                           //ImagePath = x.Image.Path
                       }).FirstOrDefault(x => x.Id == id);
        }
    }
}