using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderMapper _orderMapper;

        public OrderService(IOrderRepository orderRepository, IOrderMapper orderMapper)
        {
            _orderRepository = orderRepository;
            _orderMapper = orderMapper;
        }

        public bool AddOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto));
            }
            var order = _orderMapper.MapToEntity(orderDto);
            var inserted = _orderRepository.Insert(order);
            return inserted > 0;
        }

        public bool DeleteOrder(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var order = GetOrderById(id);
            if (order != null)
            {
                var deleted = _orderRepository.Delete(order);
                return deleted > 0;
            }
            return false;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.List();
        }

        public Order? GetOrderById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _orderRepository.Find(id);
        }

        public PaginatedList<Order> GetOrdersWithPagination(int page, int pageSize)
        {
            return PaginatedList<Order>.Create(_orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).OrderBy(x => x.OrderDate).Where(x => x.OrderStatus == OrderStatus.Pending), page, pageSize);
        }

        public IQueryable<Order> SearchOrders(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _orderRepository.GetQueryable().Include(x => x.Stock).Include(x => x.Stock.Medicine).Include(x => x.ApplicationUser).OrderBy(x => x.OrderDate).Where(x =>
                EF.Functions.Like(x.ApplicationUser.UserName, $"%{searchString}%")
                || EF.Functions.Like(x.Stock.Medicine.MedicineName, $"%{searchString}%")
                && x.OrderStatus == OrderStatus.Pending);
        }

        public PaginatedList<Order> SearchOrdersWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<Order>.Create(SearchOrders(searchString), page, pageSize);
        }

        public bool UpdateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto));
            }
            var order = _orderMapper.MapToEntity(orderDto);
            var inserted = _orderRepository.Update(order);
            return inserted > 0;
        }
    }
}
