using ConstructionSite.DTO.FrontViewModels.Maps;
using ConstructionSite.Helpers.Constants;
using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.Localization;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ConstructionSite.Controllers
{
    /// <summary>
    /// MessageController Write to Message
    /// </summary>
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly SharedLocalizationService _localizationHandle;

        public MessageController(IUnitOfWork unitOfWork,
                                  SharedLocalizationService localizationHandle,
                                  IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _localizationHandle = localizationHandle;
            _emailSender = emailSender;
        }

        /// <summary>
        /// this is Map Geolocation Dynamic Send Data
        /// To Javascrip
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var _googelMapGeolocation = _unitOfWork
                   .ContactRepository
                   .GetAll()
                   .Select(x => new Geolocation
                   {
                       Latitude = x.lat,
                       Longitude = x.lng
                   }).FirstOrDefault();
            ViewBag.la = _googelMapGeolocation.Latitude;
            ViewBag.lo = _googelMapGeolocation.Longitude;

            return View();
        }

        public IActionResult Add()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Add(MailSend emailSender)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError("", _localizationHandle.GetLocalizedHtmlString(RESOURCEKEYS.BadRequest));
            }

            _emailSender.Send(emailSender);

            return RedirectToAction("Index", "Home");
        }
    }
}