using ConstructionSite.ViwModel.AdminViewModels.Mail;

namespace ConstructionSite.Interface.Facade.Email
{
    public interface IEmailSender
    {
        void Send(MailSend mailSend);
    }
}