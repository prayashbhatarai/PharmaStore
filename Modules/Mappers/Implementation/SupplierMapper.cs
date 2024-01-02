using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class SupplierMapper : ISupplierMapper
    {
        public void Copy(SupplierDto target, Supplier source)
        {
            target.SupplierId = source.SupplierId;
            target.SupplierName = source.SupplierName;
            target.SupplierContactPerson = source.SupplierContactPerson;
            target.SupplierAddress = source.SupplierAddress;
            target.SupplierEmail = source.SupplierEmail;
            target.SupplierPhone = source.SupplierPhone;
        }

        public void Copy(Supplier target, SupplierDto source)
        {
            target.SupplierId = source.SupplierId;
            target.SupplierName = source.SupplierName;
            target.SupplierContactPerson = source.SupplierContactPerson;
            target.SupplierAddress = source.SupplierAddress;
            target.SupplierEmail = source.SupplierEmail;
            target.SupplierPhone = source.SupplierPhone;
        }

        public SupplierDto MapToDto(Supplier supplier)
        {
            SupplierDto supplierDto = new SupplierDto();
            Copy(supplierDto, supplier);
            return supplierDto;
        }

        public Supplier MapToEntity(SupplierDto supplierDto)
        {
            Supplier supplier = new Supplier();
            Copy(supplier, supplierDto);
            return supplier;
        }
    }
}
