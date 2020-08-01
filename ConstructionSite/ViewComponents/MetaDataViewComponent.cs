using ConstructionSite.DTO.AdminViewModels.ServiceViewComponent;
using ConstructionSite.Injections;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.ViewComponents
{
    public class MetaDataViewComponent:ViewComponent
    {
        string _lang;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedLocalizationService _localizationHandle;
        public MetaDataViewComponent(IUnitOfWork unitOfWork,
                                  IHttpContextAccessor httpContextAccessor,
                                  SharedLocalizationService localizationHandle)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _lang = httpContextAccessor.getLang();
            _localizationHandle = localizationHandle;
        }
        public IViewComponentResult Invoke()
        {
          
            //var result= _unitOfWork.ServiceRepository.GetAll()
            //   .Include(x=>x.SubServices)
            //   .Include(x=>x.Id)
            //   .Select(x=>new ServiceMetaData
            //   {
            //       ServiceName=x.FindName(_lang),
            //       SubServiceName=x.SubServices.Where(x=>x.Id==)
                   
            //   })
                
            return View();
        }
    }
}
