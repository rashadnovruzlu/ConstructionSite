using ConstructionSite.DTO.AdminViewModels.Testimonial;
using ConstructionSite.Entity.Models;
using ConstructionSite.Helpers.Core;
using System.Threading.Tasks;

namespace ConstructionSite.Interface.Facade.Testimonial
{
    public interface ITestimonialFacade
    {
        Task<RESULT<CustomerFeedback>> Add(CustomerAddViewModel customerAddViewModel);

        bool Update(CustomerViewUpdateModel customerViewUpdateModel);

        CustomerViewUpdateModel GetForUpdate(int id);

        bool Delete(int id);
    }
}