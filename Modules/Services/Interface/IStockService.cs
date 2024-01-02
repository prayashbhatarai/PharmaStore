using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Models.Pagination;

namespace PharmaStore.Modules.Services.Interface
{
    public interface IStockService
    {
        bool AddStock(StockDto stockDto);
        bool DeleteStock(Guid id);
        List<Stock> GetAllStocks();
        Stock? GetStockById(Guid id);
        PaginatedList<Stock> GetStocksWithPagination(int page, int pageSize);
        IQueryable<Stock> SearchStocks(string searchString);
        PaginatedList<Stock> SearchStocksWithPagination(string searchString, int page, int pageSize);
        bool UpdateStock(StockDto stockDto);
    }
}
