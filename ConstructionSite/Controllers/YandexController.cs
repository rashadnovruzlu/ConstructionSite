using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;
using ConstructionSite.ViwModel.AdminViewModels.Mail;

namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {



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


        public IActionResult SendEmail(MailSend email)
        {
            email.To = "naib.reshidov@pragmatech.az";
            email.Subject = "Salam";
            email.Body = "Salama necesen";
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(email.To);
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
            mailMessage.From = new MailAddress("residovnaib77@gmail.com");
            mailMessage.IsBodyHtml = false;


            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("residovnaib77@gmail.com", "7505020r");
                smtpClient.Send(mailMessage);
            }

            return View();
        }
    }
}