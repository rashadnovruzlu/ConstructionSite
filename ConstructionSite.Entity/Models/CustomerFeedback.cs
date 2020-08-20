using ConstructionSite.Entity.Core;

namespace ConstructionSite.Entity.Models
{
    public class CustomerFeedback : Content
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }
}