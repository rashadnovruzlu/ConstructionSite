using ConstructionSite.ViwModel.AdminViewModels.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.Interface.Facade.Email
{
  public  interface IEmailSender
    {

        void Send(MailSend mailSend);
    }
}
