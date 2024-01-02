using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface IMedicineService
    {
        bool AddMedicine(MedicineDto medicineDto, IFormFile imageFile);
        bool DeleteMedicine(Guid id);
        List<Medicine> GetAllMedicines();
        Medicine? GetMedicineById(Guid id);
        PaginatedList<Medicine> GetMedicinesWithPagination(int page, int pageSize);
        IQueryable<Medicine> SearchMedicines(string searchString);
        PaginatedList<Medicine> SearchMedicinesWithPagination(string searchString, int page, int pageSize);
        bool UpdateMedicine(MedicineDto medicineDto, IFormFile imageFile);
    }
}
