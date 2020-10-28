using ConstructionSite.ViwModel.AdminViewModels.Mail;

namespace ConstructionSite.Interface.Email
{
    public interface IEmailSenderFacade
    {
        public void SendEmail(MailSend mailSend);
    }
}