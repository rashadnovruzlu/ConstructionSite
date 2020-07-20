using ConstructionSite.DTO.FrontViewModels.Testimonial;
using ConstructionSite.Injections;
using ConstructionSite.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConstructionSite.ViewComponents
{
    public class TestimonialViewComponent:ViewComponent
    {
        private string _lang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public TestimonialViewComponent(IUnitOfWork unitOfWork,
                                         IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor=httpContextAccessor;
            _lang=_httpContextAccessor.getLang();
        }
        public IViewComponentResult Invoke()
        {
           var result= _unitOfWork.customerFeedbackRepository.GetAll()
                .Select(x=>new CustomerViewModel
                {
                    Id=x.Id,
                    Content=x.FindContent(_lang),
                    FullName=x.FullName,
                    Position=x.Position
                }).ToList();
            return View(result);

        }
    }
}
