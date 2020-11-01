using ConstructionSite.ViwModel.AdminViewModels.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

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




            return View();
        }
    }
}