using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class MedicineMapper : IMedicineMapper
    {
        public void Copy(MedicineDto target, Medicine source)
        {
            target.MedicineId = source.MedicineId;
            target.MedicineName = source.MedicineName;
            target.MedicineGenericName = source.MedicineGenericName;
            target.MedicineDescription = source.MedicineDescription;
            target.MedicineImageLink= source.MedicineImageLink;
            target.ManufacturerId= source.ManufacturerId;
            target.Manufacturer= source.Manufacturer;
            target.MedicineCategoryId= source.MedicineCategoryId;
            target.MedicineCategory = source.MedicineCategory;
        }

        public void Copy(Medicine target, MedicineDto source)
        {
            target.MedicineId = source.MedicineId;
            target.MedicineName = source.MedicineName;
            target.MedicineGenericName = source.MedicineGenericName;
            target.MedicineDescription = source.MedicineDescription;
            target.MedicineImageLink = source.MedicineImageLink;
            target.ManufacturerId = source.ManufacturerId;
            target.Manufacturer = source.Manufacturer;
            target.MedicineCategoryId = source.MedicineCategoryId;
            target.MedicineCategory = source.MedicineCategory;
        }

        public MedicineDto MapToDto(Medicine medicine)
        {
            MedicineDto medicineDto = new MedicineDto();
            Copy(medicineDto, medicine);
            return medicineDto;
        }

        public Medicine MapToEntity(MedicineDto medicineDto)
        {
            Medicine medicine = new Medicine();
            Copy(medicine, medicineDto);
            return medicine;
        }
    }
}
