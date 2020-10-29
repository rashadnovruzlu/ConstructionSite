using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using ConstructionSite.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Facade.Testimonial
{
    public class TestimonialFacade
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

            return await _unitOfWork.customerFeedbackRepository.AddAsync(customerAddViewModelResult)l;
        }
    }
}
