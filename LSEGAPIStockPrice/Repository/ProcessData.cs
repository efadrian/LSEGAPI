using LSEGAPIStockPrice.Interface;
using LSEGAPIStockPrice.Model;

namespace LSEGAPIStockPrice.Repository
{
    public class ProcessData: IProcessData
    {
        public Task<List<StockModel>> CalculateStandardDeviationAsync(List<StockModel> stockDataList)
        {
            var stockPrices = stockDataList.Select(data => data.StockPrice).ToList();

            //  media
            double media = stockPrices.Sum() / stockPrices.Count;
            // 
            double TheSumOfSquareDifferences = stockPrices.Sum(value => Math.Pow(value - media, 2));

            // mean differences
            double TheSquareDifferences = TheSumOfSquareDifferences / stockPrices.Count;

            double standardDeviation = Math.Sqrt(TheSquareDifferences);
            //
            List<StockModel> stockList = new List<StockModel>();
            
            // calculate accepted maximum value
            var acceptedMaximValue = media + (2 * standardDeviation);
            //
            foreach (var stock in stockDataList)
            {
                if (stock.StockPrice > acceptedMaximValue)
                {
                    stockList.Add(stock);
                }
            }
            return Task.FromResult(stockList);
        }
    }
}
