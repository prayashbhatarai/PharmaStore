using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Data.Entities
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; }
        
        [Required]
        public string SupplierName { get; set; } = string.Empty;
        
        [Required]
        public string SupplierContactPerson { get; set; } = string.Empty;
        
        [Required]
        public string SupplierAddress { get; set;} = string.Empty;
        
        [EmailAddress]
        public string? SupplierEmail { get; set;} = string.Empty;
        
        [Phone]
        public string? SupplierPhone { get; set;} = string.Empty;
    }
}
