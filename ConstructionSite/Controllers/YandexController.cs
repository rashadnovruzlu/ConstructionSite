using ConstructionSite.Models;
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
        private readonly NotificationMetadata _notificationMetadata;
        private readonly IWebHostEnvironment _env;
        public YandexController(NotificationMetadata notificationMetadata)
        {
            _notificationMetadata = notificationMetadata;
        }

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
        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;

        }

        public IActionResult SendEmail(string EmailViewModel)
        {
           

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_notificationMetadata.SmtpServer,
                _notificationMetadata.Port, true);
                smtpClient.Authenticate(_notificationMetadata.UserName,
                _notificationMetadata.Password);

                smtpClient.Send(null);
                smtpClient.Disconnect(true);
            }


            return View();
        }
    }
}