using PharmaStore.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Modules.Dtos
{
    public class StockDto
    {
        [Key]
        public Guid StockId { get; set; }

        [Required]
        [Display(Name = "Medicine")]
        public Guid MedicineId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public Guid SupplierId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Batch Number")]
        public string StockBatchNumber { get; set; } = string.Empty;

        [Display(Name = "Added Date")]
        public DateTime StockAddedDate { get; set; }

        [Required]
        [Display(Name = "Manufacturing Date")]
        public DateTime StockManufacturingDate { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        public DateTime StockExpiryDate { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public long StockQuantity { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public decimal StockRate { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}
