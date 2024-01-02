using Microsoft.Build.Framework;

namespace PharmaStore.Areas.Admin.Models.ViewModel
{
    public class DashboardViewModel
    {
        public int NumberOfMedicine { get; set; }
        public int NumberOfMedicineCategory { get; set; }
        public int NumberOfOrder {  get; set; }
        public int NumberOfStock {  get; set; }
    }
}
