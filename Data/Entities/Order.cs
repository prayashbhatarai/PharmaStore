using PharmaStore.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStore.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid StockId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public long OrderQuantity { get; set; }

        public decimal Amount { get; set; }

        public string? Prescription { get; set; } = string.Empty;

        public OrderStatus OrderStatus { get; set; }
        
        [ForeignKey("StockId")]
        public Stock? Stock { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
