using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierMapper _supplierMapper;

        public SupplierService(ISupplierRepository supplierRepository, ISupplierMapper supplierMapper)
        {
            _supplierRepository = supplierRepository;
            _supplierMapper = supplierMapper;
        }

        public bool AddSupplier(SupplierDto supplierDto)
        {
            if (supplierDto == null)
            {
                throw new ArgumentNullException(nameof(supplierDto));
            }
            var supplier = _supplierMapper.MapToEntity(supplierDto);
            var inserted = _supplierRepository.Insert(supplier);
            return inserted > 0;
        }

        public bool DeleteSupplier(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var supplier = GetSupplierById(id);
            if (supplier != null)
            {
                var deleted = _supplierRepository.Delete(supplier);
                return deleted > 0;
            }
            return false;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.List();
        }

        public Supplier? GetSupplierById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _supplierRepository.Find(id);
        }

        public PaginatedList<Supplier> GetSuppliersWithPagination(int page, int pageSize)
        {
            return PaginatedList<Supplier>.Create(_supplierRepository.GetQueryable(), page, pageSize);
        }

        public IQueryable<Supplier> SearchSuppliers(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _supplierRepository.GetQueryable().Where(x =>
                EF.Functions.Like(x.SupplierName, $"%{searchString}%")
                || EF.Functions.Like(x.SupplierContactPerson, $"%{searchString}%")
                || EF.Functions.Like(x.SupplierAddress, $"%{searchString}%")
                || EF.Functions.Like(x.SupplierEmail, $"%{searchString}%")
                || EF.Functions.Like(x.SupplierPhone, $"%{searchString}%"));
        }

        public PaginatedList<Supplier> SearchSuppliersWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<Supplier>.Create(SearchSuppliers(searchString), page, pageSize);
        }

        public bool UpdateSupplier(SupplierDto supplierDto)
        {
            if (supplierDto == null)
            {
                throw new ArgumentNullException(nameof(supplierDto));
            }
            var supplier = _supplierMapper.MapToEntity(supplierDto);
            var inserted = _supplierRepository.Update(supplier);
            return inserted > 0;
        }
    }
}
