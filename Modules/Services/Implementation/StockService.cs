using K4os.Hash.xxHash;
using Microsoft.EntityFrameworkCore;
using PharmaStore.Data.Entities;
using PharmaStore.Data.Repositories.Implementation;
using PharmaStore.Data.Repositories.Interface;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Implementation;
using PharmaStore.Modules.Mappers.Interface;
using PharmaStore.Modules.Models.Pagination;
using PharmaStore.Modules.Services.Interface;

namespace PharmaStore.Modules.Services.Implementation
{
    public class StockService : IStockService
    {
        public readonly IStockRepository _stockRepository;
        public readonly IStockMapper _stockMapper;

        public StockService(IStockRepository stockRepository, IStockMapper stockMapper)
        {
            _stockRepository = stockRepository;
            _stockMapper = stockMapper;
        }

        public bool AddStock(StockDto stockDto)
        {
            if (stockDto == null)
            {
                throw new ArgumentNullException(nameof(stockDto));
            }
            stockDto.StockAddedDate = DateTime.Now;
            var stock = _stockMapper.MapToEntity(stockDto);
            var inserted = _stockRepository.Insert(stock);
            return inserted > 0;
        }

        public bool DeleteStock(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            var stock = GetStockById(id);
            if (stock != null)
            {
                var deleted = _stockRepository.Delete(stock);
                return deleted > 0;
            }
            return false;
        }

        public List<Stock> GetAllStocks()
        {
            return _stockRepository.List();
        }

        public Stock? GetStockById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            return _stockRepository.Find(id);
        }

        public PaginatedList<Stock> GetStocksWithPagination(int page, int pageSize)
        {
            return PaginatedList<Stock>.Create(_stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Supplier), page, pageSize);
        }

        public IQueryable<Stock> SearchStocks(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return _stockRepository.GetQueryable().Include(x => x.Medicine).Include(x => x.Supplier).Where(x =>
                EF.Functions.Like(x.StockBatchNumber, $"%{searchString}%")
                || EF.Functions.Like(x.Medicine.MedicineName, $"%{searchString}%")
                || EF.Functions.Like(x.Supplier.SupplierName, $"%{searchString}%"));
        }

        public PaginatedList<Stock> SearchStocksWithPagination(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                throw new ArgumentException("Search string cannot be empty", nameof(searchString));
            }
            return PaginatedList<Stock>.Create(SearchStocks(searchString), page, pageSize);
        }

        public bool UpdateStock(StockDto stockDto)
        {
            if (stockDto == null)
            {
                throw new ArgumentNullException(nameof(stockDto));
            }
            var stock = _stockMapper.MapToEntity(stockDto);
            var inserted = _stockRepository.Update(stock);
            return inserted > 0;
        }
    }
}
