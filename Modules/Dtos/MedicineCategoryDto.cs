using System.ComponentModel.DataAnnotations;

namespace PharmaStore.Modules.Dtos
{
    public class MedicineCategoryDto
    {
        public Guid MedicineCategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string MedicineCategoryName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Category Description")]
        public string? MedicineCategoryDescription { get; set; } = string.Empty;
    }
}
