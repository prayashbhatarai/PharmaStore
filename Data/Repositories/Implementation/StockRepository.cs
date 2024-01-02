using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(AppDbContext context) : base(context)
        {

        }
    }
}
