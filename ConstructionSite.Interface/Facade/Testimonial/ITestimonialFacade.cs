using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Testimonial
{
    public interface ITestimonialFacade
    {
        Task<RESULT<CustomerFeedback>> Add(CustomerAddViewModel customerAddViewModel);
    }
}
