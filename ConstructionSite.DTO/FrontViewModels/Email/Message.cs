using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConstructionSite.ViwModel.FrontViewModels.Email
{
    public class Message
    {
        [UIHint("email")]
        public string From { get; set; }
        [UIHint("email")]
        public string To { get; set; }
        public string Subject { get; set; }
    }
}
