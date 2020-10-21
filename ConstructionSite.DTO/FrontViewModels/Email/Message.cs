using System.ComponentModel.DataAnnotations;

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