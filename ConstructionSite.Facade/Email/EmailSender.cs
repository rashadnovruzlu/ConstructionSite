using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System.Net;
using System.Net.Mail;

namespace ConstructionSite.Facade.Email
{
    public class EmailSender : IEmailSender
    {
        public void Send(MailSend email)
        {
            email.To = "naib.reshidov@pragmatech.az";
            email.To = "residovnaib77@gmail.com";

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email.To);
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;

            mailMessage.From = new MailAddress(email.From);
            mailMessage.IsBodyHtml = false;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 465;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("residovnaib77@gmail.com", "7505020r");
                smtpClient.Send(mailMessage);
            }
        }
        public void sendYandex(MailSend email)
        {
            email.To = "v.nurlan@pdc.az";
            email.To = "office@pdc.az";
            email.To = "sales@pdc.az";

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email.To);
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;

            mailMessage.From = new MailAddress(email.From);
            mailMessage.IsBodyHtml = false;

            SmtpClient smtpClient = new SmtpClient("smtp.yandex.com");

            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("office@pdc.az", "pdc1234567");
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
    }
}