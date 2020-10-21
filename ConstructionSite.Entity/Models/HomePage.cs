using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.Entity.Models
{
    public class HomePage
    {
        [Required]
        public int Id { get; set; }

        public virtual Image Image { get; set; }
        public int ImageId { get; set; }
    }
}