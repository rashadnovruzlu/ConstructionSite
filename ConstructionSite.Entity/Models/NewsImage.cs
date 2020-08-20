using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.Entity.Models
{
    public class NewsImage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsMain { get; set; }

        public virtual News News { get; set; }

        [Required]
        public int NewsId { get; set; }

        public virtual Image Image { get; set; }

        [Required]
        public int ImageId { get; set; }
    }
}