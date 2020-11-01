using ConstructionSite.ViwModel.AdminViewModels.Mail;

namespace ConstructionSite.Interface.Facade.Email
{
    public interface IEmailSender
    {
        void Send(MailSend mailSend, string to);
        void sendYandex(MailSend email, string to);
        void simpleSend(MailSend email, string To);
    }
}