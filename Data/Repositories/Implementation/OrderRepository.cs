using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Data.Repositories.Interface.Base;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
