using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface ISupplierMapper
    {
        public void Copy(SupplierDto target, Supplier source);
        public void Copy(Supplier target, SupplierDto source);
        public SupplierDto MapToDto(Supplier supplier);
        public Supplier MapToEntity(SupplierDto supplierDto);
    }
}
