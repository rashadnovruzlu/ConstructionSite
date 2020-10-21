using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionSite.ViwModel.FrontViewModels.Email
{
    public class EmailViewModel
    {
        public EmailViewModel(IEnumerable<string> to,
           string subject,
           string content,
           IFormFileCollection attachments = default)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }
    }
}