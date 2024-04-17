using LSEGAPIStockPrice.Model;

namespace LSEGAPIStockPrice.Interface
{
    public interface IReadExcelFile
    {
        Task<List<StockModel>> GetDataFromCVSFile(string filePath);
    }
}
