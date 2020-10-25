using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System.Net.Mail;
using System.Net;

namespace ConstructionSite.Facade.Email
{
    public class EmailSender : IEmailSender
    {
        public void Send(MailSend email)
        {
            email.To = "naib.reshidov@pragmatech.az";
            email.To = "residovnaib77@gmail.com";

            email.Subject = "Salam";
            email.Body = "Salama necesen";
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email.To);
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
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
    }
}
