using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface ISupplierService
    {
        bool AddSupplier(SupplierDto supplierDto);
        bool DeleteSupplier(Guid id);
        List<Supplier> GetAllSuppliers();
        Supplier? GetSupplierById(Guid id);
        PaginatedList<Supplier> GetSuppliersWithPagination(int page, int pageSize);
        IQueryable<Supplier> SearchSuppliers(string searchString);
        PaginatedList<Supplier> SearchSuppliersWithPagination(string searchString, int page, int pageSize);
        bool UpdateSupplier(SupplierDto supplierDto);
    }
}
