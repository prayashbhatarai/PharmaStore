using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface IMedicineMapper
    {
        public void Copy(MedicineDto target, Medicine source);
        public void Copy(Medicine target, MedicineDto source);
        public MedicineDto MapToDto(Medicine medicine);
        public Medicine MapToEntity(MedicineDto medicineDto);
    }
}
