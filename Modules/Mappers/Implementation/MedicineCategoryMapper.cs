using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class MedicineCategoryMapper : IMedicineCategoryMapper
    {
        public void Copy(MedicineCategoryDto target, MedicineCategory source)
        {
            target.MedicineCategoryId = source.MedicineCategoryId;
            target.MedicineCategoryName = source.MedicineCategoryName;
            target.MedicineCategoryDescription = source.MedicineCategoryDescription;
        }

        public void Copy(MedicineCategory target, MedicineCategoryDto source)
        {
            target.MedicineCategoryId = source.MedicineCategoryId;
            target.MedicineCategoryName = source.MedicineCategoryName;
            target.MedicineCategoryDescription = source.MedicineCategoryDescription;
        }

        public MedicineCategoryDto MapToDto(MedicineCategory medicineCategory)
        {
            MedicineCategoryDto medicineCategoryDto = new MedicineCategoryDto();
            Copy(medicineCategoryDto, medicineCategory);
            return medicineCategoryDto;
        }

        public MedicineCategory MapToEntity(MedicineCategoryDto medicineCategoryDto)
        {
            MedicineCategory medicineCategory = new MedicineCategory();
            Copy(medicineCategory, medicineCategoryDto);
            return medicineCategory;
        }
    }
}
