using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Data.Entities
{
    public class Manufacturer
    {
        [Key]
        public Guid ManufacturerId { get; set; }

        [Required]
        public string ManufacturerName { get; set; } = string.Empty;

        [Required]
        public string ManufacturerAddress { get; set; } = string.Empty;

        [EmailAddress]
        public string? ManufacturerEmail { get; set; } = string.Empty;

        [Phone]
        public string? ManufacturerPhone { get; set; } = string.Empty;
    }
}
