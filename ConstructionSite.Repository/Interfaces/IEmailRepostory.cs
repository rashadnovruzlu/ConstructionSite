using ConstructionSite.ViwModel.FrontViewModels.Email;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Interfaces
{
    public interface IEmailRepostory
    {
        Task SendEmailAsync(Message message);
    }
}
