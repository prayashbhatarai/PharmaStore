using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class MedicineRepository : Repository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(AppDbContext context) : base(context)
        {

        }
    }
}
