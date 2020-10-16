using ConstructionSite.Repository.Interfaces;
using ConstructionSite.ViwModel.FrontViewModels.Email;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Implementations
{
    public class EmailRepostory : IEmailRepostory
    {
        public Task SendEmailAsync(Message messageData)
        {
            MimeMessage message= new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            messageData.From);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            messageData.To);
            message.To.Add(to);

            message.Subject = "This is email subject";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp_address_here", 100, true);
            client.Authenticate("user_name_here", "pwd_here");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
