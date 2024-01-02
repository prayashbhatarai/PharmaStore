using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface IManufacturerMapper
    {
        public void Copy(ManufacturerDto target, Manufacturer source);
        public void Copy(Manufacturer target, ManufacturerDto source);
        public ManufacturerDto MapToDto(Manufacturer medicine);
        public Manufacturer MapToEntity(ManufacturerDto medicineDto);
    }
}
