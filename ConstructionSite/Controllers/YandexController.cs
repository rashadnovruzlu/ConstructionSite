using ConstructionSite.Repository.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(IFormFile formFile)
        {

            return View();
        }

        public IActionResult SendEmail(string EmailViewModel)
        {

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            "residovnaib77@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            "user@example.com");
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