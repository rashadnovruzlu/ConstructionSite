using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Interface.Facade.Testimonial;
using ConstructionSite.Repository.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Testimonial
{
    public class TestimonialFacade : ITestimonialFacade
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestimonialFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT<CustomerFeedback>> Add(CustomerAddViewModel customerAddViewModel)
        {
            var customerAddViewModelResult = new CustomerFeedback
            {
                ContentAz = customerAddViewModel.ContentAz,
                ContentEn = customerAddViewModel.ContentEn,
                ContentRu = customerAddViewModel.ContentRu,
                FullName = customerAddViewModel.FullName,
                Position = customerAddViewModel.Position
            };

            return await _unitOfWork.customerFeedbackRepository.AddAsync(customerAddViewModelResult);
        }

        public CustomerViewUpdateModel GetForUpdate(int id)
        {
            var resultCustomerFeedback = _unitOfWork.customerFeedbackRepository.GetAll()
                .Select(x => new CustomerViewUpdateModel
                {
                    Id = x.Id,
                    ContentAz = x.ContentAz,
                    ContentEn = x.ContentEn,
                    ContentRu = x.ContentRu,
                    FullName = x.FullName,
                    Position = x.Position
                })
                .SingleOrDefault(x => x.Id == id);
            return resultCustomerFeedback;
        }

        public bool Update(CustomerViewUpdateModel customerViewUpdateModel)
        {
            var resultCustomerFeedback = _unitOfWork.customerFeedbackRepository.Find(x => x.Id == customerViewUpdateModel.Id);
            resultCustomerFeedback.ContentAz = customerViewUpdateModel.ContentAz;
            resultCustomerFeedback.ContentRu = customerViewUpdateModel.ContentRu;
            resultCustomerFeedback.ContentEn = customerViewUpdateModel.ContentEn;
            resultCustomerFeedback.Position = customerViewUpdateModel.Position;
            resultCustomerFeedback.FullName = customerViewUpdateModel.FullName;
            return _unitOfWork.customerFeedbackRepository.Update(resultCustomerFeedback).IsDone;
        }

        public bool Delete(int id)
        {
            var resultCustomerFeedback = _unitOfWork.customerFeedbackRepository.Find(x => x.Id == id);
           return _unitOfWork.customerFeedbackRepository.Delete(resultCustomerFeedback).IsDone;
           
        }
    }
}