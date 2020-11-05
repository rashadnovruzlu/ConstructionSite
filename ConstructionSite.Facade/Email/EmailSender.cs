using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System.Net;
using System.Net.Mail;

namespace ConstructionSite.Facade.Email
{
    public class EmailSender : IEmailSender
    {
        public void Send(MailSend email, string TO)
        {
            // email.To = "naib.reshidov@pragmatech.az";

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(TO);
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;

                mailMessage.From = new MailAddress(email.From);
                mailMessage.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 465;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("r77@gmail.com", "");
                    smtpClient.Send(mailMessage);
                }
            }
            catch (System.Exception ex)
            {
                var exs = ex.Message;
            }
        }

        public void simpleSend(MailSend email, string To)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.yandex.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("office@pdc.az", "pdc1234567");
                    MailMessage Message1 = new MailMessage(email.From, To, email.Subject, email.Body);

                    smtpClient.Send(Message1);
                }
            }
            catch
            {
            }
        }

        public void sendYandex(MailSend email, string To)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(To);

                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;

                mailMessage.From = new MailAddress(email.From);
                mailMessage.IsBodyHtml = false;

                using (SmtpClient smtpClient = new SmtpClient("smtp.yandex.com"))
                {
                    smtpClient.Port = 587;

                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("office@pdc.az", "pdc1234567");
                    smtpClient.Send(mailMessage);
                }
            }
            catch (System.Exception ex)
            {
                var exs = ex.Message;
            }
        }
    }
}