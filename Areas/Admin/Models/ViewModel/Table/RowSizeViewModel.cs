namespace PharmaStore.Areas.Admin.Models.ViewModel.Table
{
    public class RowSizeViewModel
    {
        public RowSizeViewModel(string controller, string action, int pageSize)
        {
            Controller = controller;
            Action = action;
            PageSize = pageSize;
        }

        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public int PageSize { get; set; }
    }
}
