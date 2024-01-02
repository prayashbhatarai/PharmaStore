using Org.BouncyCastle.Asn1.X509;
using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class OrderMapper : IOrderMapper
    {
        public void Copy(OrderDto target, Order source)
        {
            target.OrderId = source.OrderId;
            target.StockId = source.StockId;
            target.UserId = source.UserId;
            target.OrderDate = source.OrderDate;
            target.OrderQuantity = source.OrderQuantity;
            target.Amount = source.Amount;
            target.Prescription = source.Prescription;
            target.OrderStatus = source.OrderStatus;
            target.Stock = source.Stock;
            target.ApplicationUser = source.ApplicationUser;
        }

        public void Copy(Order target, OrderDto source)
        {
            target.OrderId = source.OrderId;
            target.StockId = source.StockId;
            target.UserId = source.UserId;
            target.OrderDate = source.OrderDate;
            target.OrderQuantity = source.OrderQuantity;
            target.Amount = source.Amount;
            target.Prescription = source.Prescription;
            target.OrderStatus = source.OrderStatus;
            target.Stock = source.Stock;
            target.ApplicationUser = source.ApplicationUser;
        }

        public OrderDto MapToDto(Order order)
        {
            OrderDto orderDto = new OrderDto();
            Copy(orderDto, order);
            return orderDto;
        }

        public Order MapToEntity(OrderDto orderDto)
        {
            Order order = new Order();
            Copy(order, orderDto);
            return order;
        }
    }
}
