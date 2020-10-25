using ConstructionSite.Interface.Facade.Email;
using ConstructionSite.Repository.Abstract;
using ConstructionSite.ViwModel.AdminViewModels.Mail;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionSite.ViewComponents
{
    public class MessageViewComponent : ViewComponent
    {
        private string _lang;
        private readonly IUnitOfWork _unitOfWork;


        public MessageViewComponent(IUnitOfWork unitOfWork
                                )
        {
            _unitOfWork = unitOfWork;

        }

        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}