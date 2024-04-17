using LSEGAPIStockPrice.Model;

namespace LSEGAPIStockPrice.Interface
{
    public interface IProcessData
    {
        Task<List<StockModel>> CalculateStandardDeviationAsync(List<StockModel> stockDataList);
    }
}
