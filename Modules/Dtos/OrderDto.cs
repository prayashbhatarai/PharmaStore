using PharmaStore.Data.Entities;
using PharmaStore.Data.Identity;

namespace PharmaStore.Modules.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid StockId { get; set; }
        public string? UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public long OrderQuantity { get; set; }

        public decimal Amount { get; set; }

        public string? Prescription { get; set; } = string.Empty;

        public OrderStatus OrderStatus { get; set; }
        
        public Stock? Stock { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
