using ConstructionSite.DTO.FrontViewModels.Service;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Cores.Queryes
{
    public static class ServiceQuery
    {
        public static SingleService ServiceRepositoryQuery(this IUnitOfWork _unitOfWork,int id,string _lang)
        {
          return  _unitOfWork.ServiceRepository
                       .GetAll()
                       .Include(x => x.Image)
                       .Select(x => new SingleService
                       {
                           Id = x.Id,
                           Name = x.FindName(_lang),
                           Title = x.FindTitle(_lang),
                           ImagePath = x.Image.Path
                       }).FirstOrDefault(x => x.Id == id);
        }
    }
}
