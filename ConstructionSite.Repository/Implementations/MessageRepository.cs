using ConstructionSite.Entity.Data;
using ConstructionSite.Entity.Models;
using ConstructionSite.Repository.Concreate;
using ConstructionSite.Repository.Interfaces;

namespace ConstructionSite.Repository.Implementations
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ConstructionDbContext context) : base(context)
        {
        }
    }
}