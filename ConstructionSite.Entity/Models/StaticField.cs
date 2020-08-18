using System.ComponentModel.DataAnnotations;

namespace ConstructionSite.Entity.Models
{
    public class StaticField
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Key { get; set; }

        [Required]
        [MaxLength(250)]
        public string Value { get; set; }
    }
}