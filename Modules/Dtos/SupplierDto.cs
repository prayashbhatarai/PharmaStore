using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Modules.Dtos
{
    public class SupplierDto
    {
        public Guid SupplierId { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Supplier Contact Person Name")]
        public string SupplierContactPerson { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Supplier Address")]
        public string SupplierAddress { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Supplier Email")]
        public string? SupplierEmail { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Supplier Phone")]
        public string? SupplierPhone { get; set; } = string.Empty;
    }
}
