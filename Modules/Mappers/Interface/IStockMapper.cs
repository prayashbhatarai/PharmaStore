using PharmaStore.Data.Entities;
using PharmaStore.Modules.Dtos;

namespace PharmaStore.Modules.Mappers.Interface
{
    public interface IStockMapper
    {
        public void Copy(StockDto target, Stock source);
        public void Copy(Stock target, StockDto source);
        public StockDto MapToDto(Stock stock);
        public Stock MapToEntity(StockDto stockDto);
    }
}
