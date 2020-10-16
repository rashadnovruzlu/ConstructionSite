using ConstructionSite.Helpers.Emails;
using ConstructionSite.ViwModel.FrontViewModels.Email;
using Microsoft.EntityFrameworkCore.Internal;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ConstructionSite.Repository.Implementations
{
    public class EmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(EmailViewModel messageYandexViewModelEmailSender)
        {
            var emailMessage = CreateEmailMessage(messageYandexViewModelEmailSender);

            await SendAsync(await emailMessage).ConfigureAwait(false);
        }

        private async Task<MimeMessage> CreateEmailMessage(EmailViewModel message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Al Mursheed", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    await using (var ms = new MemoryStream())
                    {
                        await attachment.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes,
                        ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using var client = new SmtpClient();
            try
            {
                client.ServerCertificateValidationCallback =
                    (sender, certificate, certChainType, errors) => true;
                //Remove any OAuth functionality as we won't be using it.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                //The last parameter here is to use SSL (Which you should!)
                await client.ConnectAsync(_emailConfig.SmtpServer, 587, false)
                    .ConfigureAwait(false);
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password)
                    .ConfigureAwait(false);
                await client.SendAsync(mailMessage)
                    .ConfigureAwait(false);
            }
            catch (Exception error)
            {
                //log an error message or throw an exception or both.
                throw new Exception(error.Message);
            }
            finally
            {
                await client.DisconnectAsync(true)
                    .ConfigureAwait(false);
                client.Dispose();
            }
        }
    }
}