using ConstructionSite.ViwModel.FrontViewModels.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Repository.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailViewModel yandexViewModelEmailSender);
    }
}
