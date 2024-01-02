using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {

        }
    }
}
