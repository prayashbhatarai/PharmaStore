using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
