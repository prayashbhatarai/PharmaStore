using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface IManufacturerService
    {
        bool AddManufacturer(ManufacturerDto manufacturerDto);
        bool DeleteManufacturer(Guid id);
        List<Manufacturer> GetAllManufacturers();
        Manufacturer? GetManufacturerById(Guid id);
        PaginatedList<Manufacturer> GetManufacturersWithPagination(int page, int pageSize);
        IQueryable<Manufacturer> SearchManufacturers(string searchString);
        PaginatedList<Manufacturer> SearchManufacturersWithPagination(string searchString, int page, int pageSize);
        bool UpdateManufacturer(ManufacturerDto manufacturerDto);
    }
}
