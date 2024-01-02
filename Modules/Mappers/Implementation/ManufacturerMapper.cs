using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class ManufacturerMapper : IManufacturerMapper
    {
        public void Copy(ManufacturerDto target, Manufacturer source)
        {
            target.ManufacturerId = source.ManufacturerId;
            target.ManufacturerName = source.ManufacturerName;
            target.ManufacturerAddress = source.ManufacturerAddress;
            target.ManufacturerEmail = source.ManufacturerEmail;
            target.ManufacturerPhone = source.ManufacturerPhone;
        }

        public void Copy(Manufacturer target, ManufacturerDto source)
        {
            target.ManufacturerId = source.ManufacturerId;
            target.ManufacturerName = source.ManufacturerName;
            target.ManufacturerAddress = source.ManufacturerAddress;
            target.ManufacturerEmail = source.ManufacturerEmail;
            target.ManufacturerPhone = source.ManufacturerPhone;
        }

        public ManufacturerDto MapToDto(Manufacturer manufacturer)
        {
            ManufacturerDto manufacturerDto = new ManufacturerDto();
            Copy(manufacturerDto, manufacturer);
            return manufacturerDto;
        }

        public Manufacturer MapToEntity(ManufacturerDto manufacturerDto)
        {
            Manufacturer manufacturer = new Manufacturer();
            Copy(manufacturer, manufacturerDto);
            return manufacturer;
        }
    }
}
