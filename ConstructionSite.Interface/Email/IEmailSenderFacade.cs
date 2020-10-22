using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Interface.Email
{
    public interface IEmailSenderFacade
    {
        public void SendEmail(MailSend mailSend);
        
    }
}
