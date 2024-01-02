using PharmaStore.Data.Context;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation.Base;
using PharmaStore.Data.Repositories.Interface;

namespace PharmaStore.Data.Repositories.Implementation
{
    public class MedicineCategoryRepository : Repository<MedicineCategory>, IMedicineCategoryRepository
    {
        public MedicineCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
