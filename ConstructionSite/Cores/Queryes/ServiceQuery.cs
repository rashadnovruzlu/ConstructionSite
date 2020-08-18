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
        public static SingleService ServiceRepositoryQuery(this IUnitOfWork unitOfWork,int id,string lang)
        {
          var SingleServiceResult=  unitOfWork.ServiceRepository
                       .GetAll()
                       .Include(x => x.Image)
                       .Select(x => new SingleService
                       {
                           Id = x.Id,
                           Name = x.FindName(lang),
                           Title = x.FindTitle(lang),
                           ImagePath = x.Image.Path
                       }).FirstOrDefault(x => x.Id == id);
            return SingleServiceResult;
        }
    }
}
