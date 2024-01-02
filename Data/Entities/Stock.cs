using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStore.Data.Entities
{
    public class Stock
    {
        [Key]
        public Guid StockId { get; set; }

        [Required]
        public Guid MedicineId { get; set; }

        [Required]
        public Guid SupplierId { get; set; }

        [Required]
        [StringLength(10)]
        public string StockBatchNumber { get; set; } = string.Empty;

        public DateTime StockAddedDate { get; set; }

        [Required]
        public DateTime StockManufacturingDate { get; set; }

        [Required]
        public DateTime StockExpiryDate { get; set; }

        [Required]
        public long StockQuantity { get; set; }

        [Required]
        public decimal StockRate { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine? Medicine { get; set; }
        
        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}