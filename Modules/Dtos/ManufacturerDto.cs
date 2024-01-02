using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Modules.Dtos
{
    public class ManufacturerDto
    {
        public Guid ManufacturerId { get; set; }

        [Required]
        [Display(Name = "Manufacturer Name")]
        public string ManufacturerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Manufacturer Address")]
        public string ManufacturerAddress { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Manufacturer Email")]
        public string? ManufacturerEmail { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Manufacturer Phone")]
        public string? ManufacturerPhone { get; set; } = string.Empty;
    }
}
