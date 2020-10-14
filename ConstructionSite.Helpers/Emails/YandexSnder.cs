using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Helpers.Emails
{
    public class YandexSnder
    {


        public async Task SendEmail(string email = "kimse@yandex.ru", string subject = "Nothing", string message = "Test")
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("kimse", "login@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("",email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru",25,false);
                await client.AuthenticateAsync("login@yandex.ru", "password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }

}
