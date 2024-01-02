using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Helpers.Interface;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class MedicineService : IMedicineService
    {
        private readonly string uploadpath = "uploads\\medicine";
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineMapper _medicineMapper;
        private readonly IFileHelper _fileHelper;

        public MedicineService(IMedicineRepository medicineRepository, IMedicineMapper medicineMapper, IFileHelper fileHelper)
        {
            _medicineRepository = medicineRepository ?? throw new ArgumentNullException(nameof(medicineRepository));
            _medicineMapper = medicineMapper ?? throw new ArgumentNullException(nameof(medicineMapper));
            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
        }

        public bool AddMedicine(MedicineDto medicineDto, IFormFile imageFile)
        {
            if (medicineDto == null)
            {
                throw new ArgumentNullException(nameof(medicineDto));
            }
            medicineDto.MedicineImageLink = _fileHelper.SaveFile(uploadpath, imageFile);
            var medicine = _medicineMapper.MapToEntity(medicineDto);
            var inserted = _medicineRepository.Insert(medicine);
            return inserted > 0;
        }

        public bool DeleteMedicine(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var medicine = GetMedicineById(id);
            if (medicine != null)
            {
                _fileHelper.DeleteFile(uploadpath, medicine.MedicineImageLink);
                var deleted = _medicineRepository.Delete(medicine);
                return deleted > 0;
            }
            return false;
        }

        public List<Medicine> GetAllMedicines()
        {
            return _medicineRepository.GetQueryable().Include(x => x.Manufacturer).Include(x => x.MedicineCategory).ToList();
        }

        public Medicine? GetMedicineById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _medicineRepository.Find(id);
        }

        public PaginatedList<Medicine> GetMedicinesWithPagination(int page, int pageSize)
        {
            return PaginatedList<Medicine>.Create(_medicineRepository.GetQueryable().Include(x => x.Manufacturer).Include(x => x.MedicineCategory), page, pageSize);
        }

        public IQueryable<Medicine> SearchMedicines(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _medicineRepository.GetQueryable().Include(x => x.Manufacturer).Include(x => x.MedicineCategory).Where(x =>
                EF.Functions.Like(x.MedicineName, $"%{searchString}%")
                || EF.Functions.Like(x.MedicineDescription, $"%{searchString}%"));
        }

        public PaginatedList<Medicine> SearchMedicinesWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<Medicine>.Create(SearchMedicines(searchString), page, pageSize);
        }

        public bool UpdateMedicine(MedicineDto medicineDto, IFormFile imageFile)
        {
            if (medicineDto == null)
            {
                throw new ArgumentNullException(nameof(medicineDto));
            }
            var premedicine = _medicineRepository.GetEnumerable().Where(x => x.MedicineId == medicineDto.MedicineId).Select(x => x.MedicineImageLink).ToString();
            if(_fileHelper.DeleteFile(uploadpath,premedicine))
            {
                medicineDto.MedicineImageLink = _fileHelper.SaveFile(uploadpath, imageFile);
            }
            var medicine = _medicineMapper.MapToEntity(medicineDto);
            var updated = _medicineRepository.Update(medicine);
            return updated > 0;
        }
    }
}
