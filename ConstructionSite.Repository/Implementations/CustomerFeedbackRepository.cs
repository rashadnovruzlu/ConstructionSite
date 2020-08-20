using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class CustomerFeedbackRepository : GenericRepository<CustomerFeedback>, ICustomerFeedbackRepository
    {
        public CustomerFeedbackRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}