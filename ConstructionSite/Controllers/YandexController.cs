using ConstructionSite.Helpers.Emails;
using ConstructionSite.ViwModel.FrontViewModels.Email;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail(EmailViewModel yandexViewModelEmailSender)
        {
            yandexViewModelEmailSender.Body = "Salam";
            yandexViewModelEmailSender.To = "residovnaib77@gmail.com";
            yandexViewModelEmailSender.Subject = "nese";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("naib.reshidov@pragmatech.az");
            mailMessage.To.Add(yandexViewModelEmailSender.To);
            mailMessage.Subject = yandexViewModelEmailSender.Subject;
            mailMessage.Body = yandexViewModelEmailSender.Body;

            mailMessage.IsBodyHtml = false;
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("naib.reshidov@pragmatech.az", "7505020r");
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(mailMessage);
            }
            return View();
        }
    }
}