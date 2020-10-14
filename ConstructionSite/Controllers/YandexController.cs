using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Helpers.Emails;
using ConstructionSite.ViwModel.FrontViewModels.Yandex;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {
        public IActionResult Index()
        {
            Send("residovnaib@gmail.com", "Salam", "Kimse");
            return View();
        }
        //public interface IEmailService
        //{
        //    void Send(string from, string to, string subject, string html);
        //}



        public void Send(string email, string subject, string message)
        {

            var emailData = new MimeMessage();
            emailData.From.Add(new MailboxAddress("Naib", "NaibResidov@yandex.ru"));
            emailData.To.Add(new MailboxAddress("", email));
            emailData.Subject = subject;
            emailData.Body = new TextPart(TextFormat.Html) { Text = message };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Send(emailData);
            smtp.Disconnect(true);
        }
    }
    //public async Task<IActionResult> SendEmail(YandexViewModel yandexViewModelEmailSender)
    //{
    //    YandexSnder yandexSnder = new YandexSnder();
    //    yandexViewModelEmailSender.Email = "NaibResidov@yandex.ru";
    //    yandexViewModelEmailSender.Message = "Nothig";
    //    yandexViewModelEmailSender.Subject = "qoqal";
    //    await yandexSnder.SendEmail(yandexViewModelEmailSender.Email, yandexViewModelEmailSender.Subject, yandexViewModelEmailSender.Message);
    //    return View();
    //}
}

