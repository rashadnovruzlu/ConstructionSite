using ConstructionSite.ViwModel.FrontViewModels.Email;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailViewModel yandexViewModelEmailSender);
    }
}