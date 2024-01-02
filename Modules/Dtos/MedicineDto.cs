using PharmaStore.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStore.Modules.Dtos
{
    public class MedicineDto
    {
        [Key]
        public Guid MedicineId { get; set; }

        [Required]
        [Display(Name = "Medicine Name")]
        public string MedicineName { get; set; } = string.Empty;

        [Display(Name = "Medicine Generic Name")]
        public string? MedicineGenericName { get; set; } = string.Empty;

        [StringLength(250)]
        [Display(Name = "Medicine Description")]
        public string? MedicineDescription { get; set; } = string.Empty;

        public string? MedicineImageLink { get; set; } = string.Empty;

        [Required]
        [Display(Name ="Medicine Manufacturer")]
        public Guid ManufacturerId { get; set; }

        public Manufacturer? Manufacturer { get; set; }
        public Guid MedicineCategoryId { get; set; }
        public MedicineCategory? MedicineCategory { get; set; }
    }
}