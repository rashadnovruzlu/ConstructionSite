
using ConstructionSite.Helpers.Content;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Helpers.Interfaces
{
   public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
