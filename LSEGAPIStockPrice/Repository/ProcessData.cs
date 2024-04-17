using LSEGAPIStockPrice.Interface;
using LSEGAPIStockPrice.Model;

namespace LSEGAPIStockPrice.Repository
{
    public class ProcessData: IProcessData
    {
        public Task<List<StockModel>> CalculateStandardDeviationAsync(List<StockModel> stockDataList)
        {
            // get list with stock prices
            var stockPrices = stockDataList.Select(data => data.StockPrice).ToList();

            //  calculate media
            double media = stockPrices.Sum() / stockPrices.Count;

            // sum of squared difference between value and the media
            double TheSumOfSquareDifferences = stockPrices.Sum(value => Math.Pow(value - media, 2));

            // average differences
            double TheSquareDifferences = TheSumOfSquareDifferences / stockPrices.Count;
            
            // get deviation
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
