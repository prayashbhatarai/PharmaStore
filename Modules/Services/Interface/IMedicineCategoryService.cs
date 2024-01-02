using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface IMedicineCategoryService
    {
        bool AddCategory(MedicineCategoryDto medicineCategoryDto);
        bool DeleteCategory(Guid id);
        List<MedicineCategory> GetAllCategory();
        MedicineCategory? GetCategoryById(Guid id);
        PaginatedList<MedicineCategory> GetCategoriesWithPagination(int page, int pageSize);
        IQueryable<MedicineCategory> SearchCategories(string searchString);
        PaginatedList<MedicineCategory> SearchCategoriesWithPagination(string searchString, int page, int pageSize);
        bool UpdateCategory(MedicineCategoryDto medicineCategoryDto);
    }
}
