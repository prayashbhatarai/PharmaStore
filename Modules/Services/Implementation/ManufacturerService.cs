using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IManufacturerMapper _manufacturerMapper;

        public ManufacturerService(IManufacturerRepository manufacturerRepository, IManufacturerMapper manufacturerMapper)
        {
            _manufacturerRepository = manufacturerRepository;
            _manufacturerMapper = manufacturerMapper;
        }

        public bool AddManufacturer(ManufacturerDto manufacturerDto)
        {
            if (manufacturerDto == null)
            {
                throw new ArgumentNullException(nameof(manufacturerDto));
            }
            var manufacturer = _manufacturerMapper.MapToEntity(manufacturerDto);
            var inserted = _manufacturerRepository.Insert(manufacturer);
            return inserted > 0;
        }

        public bool DeleteManufacturer(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var manufacturer = GetManufacturerById(id);
            if (manufacturer != null)
            {
                var deleted = _manufacturerRepository.Delete(manufacturer);
                return deleted > 0;
            }
            return false;
        }

        public List<Manufacturer> GetAllManufacturers()
        {
            return _manufacturerRepository.List();
        }

        public Manufacturer? GetManufacturerById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _manufacturerRepository.Find(id);
        }

        public PaginatedList<Manufacturer> GetManufacturersWithPagination(int page, int pageSize)
        {
            return PaginatedList<Manufacturer>.Create(_manufacturerRepository.GetQueryable(), page, pageSize);
        }

        public IQueryable<Manufacturer> SearchManufacturers(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _manufacturerRepository.GetQueryable().Where(x =>
                EF.Functions.Like(x.ManufacturerName, $"%{searchString}%")
                || EF.Functions.Like(x.ManufacturerAddress, $"%{searchString}%")
                || EF.Functions.Like(x.ManufacturerEmail, $"%{searchString}%")
                || EF.Functions.Like(x.ManufacturerPhone, $"%{searchString}%"));
        }

        public PaginatedList<Manufacturer> SearchManufacturersWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<Manufacturer>.Create(SearchManufacturers(searchString), page, pageSize);
        }

        public bool UpdateManufacturer(ManufacturerDto manufacturerDto)
        {
            if (manufacturerDto == null)
            {
                throw new ArgumentNullException(nameof(manufacturerDto));
            }
            var manufacturer = _manufacturerMapper.MapToEntity(manufacturerDto);
            var inserted = _manufacturerRepository.Update(manufacturer);
            return inserted > 0;
        }
    }
}
