using ConstructionSite.Repository.Interfaces;
using ConstructionSite.ViwModel.FrontViewModels.Email;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Implementations
{
    public class EmailRepostory : IEmailRepostory
    {
        public async Task SendEmailAsync(Message messageData)
        {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            messageData.From);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            messageData.To);
            message.To.Add(to);

            message.Subject = messageData.Subject;

            SmtpClient client = new SmtpClient();
            await client.ConnectAsync("smtp_address_here", 100, true);
            await client.AuthenticateAsync("user_name_here", "pwd_here");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}