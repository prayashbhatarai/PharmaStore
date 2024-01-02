using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface IMedicineCategoryMapper
    {
        public void Copy(MedicineCategoryDto target, MedicineCategory source);
        public void Copy(MedicineCategory target, MedicineCategoryDto source);
        public MedicineCategoryDto MapToDto(MedicineCategory medicineCategory);
        public MedicineCategory MapToEntity(MedicineCategoryDto medicineCategoryDto);
    }
}
