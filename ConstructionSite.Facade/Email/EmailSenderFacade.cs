using ConstructionSite.Interface.Email;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System.Net.Mail;
using System.Net;

namespace ConstructionSite.Facade.Email
{
    public class EmailSenderFacade : IEmailSenderFacade
    {
        public void SendEmail(MailSend mailSend)
        {
            try
            {
                mailSend.To = "naib.reshidov@pragmatech.az";
                mailSend.To = "naib.reshidov@pragmatech.az";
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(mailSend.To);
                mailMessage.Subject = mailSend.Subject;
                mailMessage.Body = mailSend.Body;
                mailMessage.From = new MailAddress("residovnaib77@gmail.com");
                mailMessage.IsBodyHtml = false;


                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("residovnaib77@gmail.com", "7505020r");
                    smtpClient.Send(mailMessage);
                }
            }
            catch
            {


            }
        }
    }
}
