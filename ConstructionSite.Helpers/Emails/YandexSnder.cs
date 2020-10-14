﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
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
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                await client.AuthenticateAsync("smtp.gmail.com", "7505020r");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }

}
