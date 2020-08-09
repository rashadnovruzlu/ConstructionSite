using ConstructionSite.DTO.FrontViewModels.SubService;
using ConstructionSite.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstructionSite.Cores.Queryes
{
    public static class SubServiceQuery
    {
        public static ServiceSubServiceImage SubServiceImageRepositoryQuery(this IUnitOfWork _unitOfWork,int id,string _lang)
        {
          return  _unitOfWork.SubServiceImageRepository.GetAll()
               .Include(x => x.SubService.Service)
               .Include(x => x.SubService)
               .Include(x => x.SubService.SubServiceImages)
               .Where(y => y.SubService.ServiceId == id)
               .Select(x => new ServiceSubServiceImage
               {
                   id = x.Id,
                   SubServiceID = x.SubServiceId,
                   Content = x.SubService.FindContent(_lang),
                   Name = x.SubService.FindName(_lang),
                   Images = x.SubService.SubServiceImages.Select(x => x.Image.Path).ToList()
               }).OrderByDescending(x => x.id)
               .FirstOrDefault();
        }
    }
}
