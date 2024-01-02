namespace PharmaStore.Areas.Admin.Models.ViewModel.Table
{
    public class PaginationViewModel
    {
        public PaginationViewModel(string controller, string action, bool hasPreviousPage, bool hasNextPage, int pageIndex, int pageSize, int totalPages, string? search, int totalItem, int count)
        {
            Controller = controller;
            Action = action;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = totalPages;
            Search = search;
            TotalItem = totalItem;
            Count = count;
        }

        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
        public int TotalItem { get; set; }
        public int Count { get; set; }
    }
}
