using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface IOrderMapper
    {
        public void Copy(OrderDto target, Order source);
        public void Copy(Order target, OrderDto source);
        public OrderDto MapToDto(Order order);
        public Order MapToEntity(OrderDto orderDto);
    }
}
