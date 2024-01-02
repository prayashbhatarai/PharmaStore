using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class MedicineCategoryService : IMedicineCategoryService
    {
        private readonly IMedicineCategoryRepository _medicineCategoryRepository;
        private readonly IMedicineCategoryMapper _medicineCategoryMapper;

        public MedicineCategoryService(IMedicineCategoryRepository medicineCategoryRepository, IMedicineCategoryMapper medicineCategoryMapper)
        {
            _medicineCategoryRepository = medicineCategoryRepository;
            _medicineCategoryMapper = medicineCategoryMapper;
        }

        public bool AddCategory(MedicineCategoryDto medicineCategoryDto)
        {
            if (medicineCategoryDto == null)
            {
                throw new ArgumentNullException(nameof(medicineCategoryDto));
            }
            var medicineCategory = _medicineCategoryMapper.MapToEntity(medicineCategoryDto);
            var inserted = _medicineCategoryRepository.Insert(medicineCategory);
            return inserted > 0;
        }

        public bool DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var medicineCategory = GetCategoryById(id);
            if (medicineCategory != null)
            {
                var deleted = _medicineCategoryRepository.Delete(medicineCategory);
                return deleted > 0;
            }
            return false;
        }

        public List<MedicineCategory> GetAllCategory()
        {
            return _medicineCategoryRepository.List();
        }

        public PaginatedList<MedicineCategory> GetCategoriesWithPagination(int page, int pageSize)
        {
            return PaginatedList<MedicineCategory>.Create(_medicineCategoryRepository.GetQueryable(), page, pageSize);
        }

        public MedicineCategory? GetCategoryById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _medicineCategoryRepository.Find(id);
        }

        public IQueryable<MedicineCategory> SearchCategories(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _medicineCategoryRepository.GetQueryable().Where(x =>
                EF.Functions.Like(x.MedicineCategoryName, $"%{searchString}%")
                || EF.Functions.Like(x.MedicineCategoryDescription, $"%{searchString}%"));
        }

        public PaginatedList<MedicineCategory> SearchCategoriesWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<MedicineCategory>.Create(SearchCategories(searchString), page, pageSize);
        }

        public bool UpdateCategory(MedicineCategoryDto medicineCategoryDto)
        {
            if (medicineCategoryDto == null)
            {
                throw new ArgumentNullException(nameof(medicineCategoryDto));
            }
            var medicineCategory = _medicineCategoryMapper.MapToEntity(medicineCategoryDto);
            var updated = _medicineCategoryRepository.Update(medicineCategory);
            return updated > 0;
        }
    }
}
