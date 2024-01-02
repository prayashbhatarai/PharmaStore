using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Data.Entities
{
    public class MedicineCategory
    {
        [Key]
        public Guid MedicineCategoryId { get; set; }

        [Required]
        public string MedicineCategoryName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MedicineCategoryDescription { get; set; } = string.Empty;
    }
}
