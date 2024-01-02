using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface IOrderService
    {
        bool AddOrder(OrderDto orderDto);
        bool DeleteOrder(Guid id);
        List<Order> GetAllOrders();
        Order? GetOrderById(Guid id);
        PaginatedList<Order> GetOrdersWithPagination(int page, int pageSize);
        IQueryable<Order> SearchOrders(string searchString);
        PaginatedList<Order> SearchOrdersWithPagination(string searchString, int page, int pageSize);
        bool UpdateOrder(OrderDto orderDto);
    }
}
