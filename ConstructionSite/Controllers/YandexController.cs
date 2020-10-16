using ConstructionSite.ViwModel.FrontViewModels.Email;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail( yandexViewModelEmailSender)
        {

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            "residovnaib77@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            "naib.reshidov@pragmatech.az");
            message.To.Add(to);

            message.Subject = "This is email subject";

            SmtpClient client = new SmtpClient();
            client.Connect("smtp_address_here", 100, true);
            client.Authenticate("user_name_here", "pwd_here");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return View();
        }
    }
}