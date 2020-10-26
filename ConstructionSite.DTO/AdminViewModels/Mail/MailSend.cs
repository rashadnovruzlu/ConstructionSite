using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionSite.ViwModel.AdminViewModels.Mail
{
    public class MailSend
    {
        public  string To { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
    }
}
