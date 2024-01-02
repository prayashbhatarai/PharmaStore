using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;
using PharmaStore.Modules.Mappers.Interface;

namespace PharmaStore.Modules.Mappers.Implementation
{
    public class StockMapper : IStockMapper
    {
        public void Copy(StockDto target, Stock source)
        {
            target.StockId = source.StockId;
            target.MedicineId = source.MedicineId;
            target.SupplierId = source.SupplierId;
            target.StockBatchNumber = source.StockBatchNumber;
            target.StockAddedDate = source.StockAddedDate;
            target.StockManufacturingDate = source.StockManufacturingDate;
            target.StockExpiryDate = source.StockExpiryDate;
            target.StockQuantity = source.StockQuantity;
            target.StockRate = source.StockRate;
            target.Medicine = source.Medicine;
            target.Supplier = source.Supplier;
        }

        public void Copy(Stock target, StockDto source)
        {
            target.StockId = source.StockId;
            target.MedicineId = source.MedicineId;
            target.SupplierId = source.SupplierId;
            target.StockBatchNumber = source.StockBatchNumber;
            target.StockAddedDate = source.StockAddedDate;
            target.StockManufacturingDate = source.StockManufacturingDate;
            target.StockExpiryDate = source.StockExpiryDate;
            target.StockQuantity = source.StockQuantity;
            target.StockRate = source.StockRate;
            target.Medicine = source.Medicine;
            target.Supplier = source.Supplier;
        }

        public StockDto MapToDto(Stock stock)
        {
            StockDto stockDto = new StockDto();
            Copy(stockDto, stock);
            return stockDto;
        }

        public Stock MapToEntity(StockDto stockDto)
        {
            Stock stock = new Stock();
            Copy(stock, stockDto);
            return stock;
        }
    }
}
