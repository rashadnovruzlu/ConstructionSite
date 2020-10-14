using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstructionSite.Helpers.Emails;
using ConstructionSite.ViwModel.FrontViewModels.Yandex;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.Controllers
{
    public class YandexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendEmail(YandexViewModel yandexViewModelEmailSender)
        {
            YandexSnder yandexSnder = new YandexSnder();
            await yandexSnder.SendEmail(yandexViewModelEmailSender.Email, yandexViewModelEmailSender.Subject, yandexViewModelEmailSender.Message);
            return View();
        }
    }
}
