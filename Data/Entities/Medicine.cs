using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaStore.Data.Entities
{
    public class Medicine
    {
        [Key]
        public Guid MedicineId { get; set; }

        [Required]
        public string MedicineName { get; set; } = string.Empty;
        
        public string? MedicineGenericName { get; set; } = string.Empty;
        
        [StringLength(250)]
        public string? MedicineDescription { get; set; } = string.Empty;
        
        public string? MedicineImageLink { get; set; } = string.Empty;
        
        [Required]
        public Guid ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer? Manufacturer { get; set; }
        [Required]

        public Guid MedicineCategoryId { get; set; }

        [ForeignKey("MedicineCategoryId")]
        public MedicineCategory? MedicineCategory { get; set; }
    }
}
