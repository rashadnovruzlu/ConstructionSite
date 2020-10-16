using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Helpers.Emails
{
    public class YandexSnder
    {


        public async Task SendEmail(string email, string subject, string message)
        {

          
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("kimse", "NaibResidov@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Plain) { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("Naib Residov", "7505020r");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }

}
