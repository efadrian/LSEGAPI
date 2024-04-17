using LSEGAPIStockPrice.Interface;
using LSEGAPIStockPrice.Model;
using System.Globalization;

namespace LSEGAPIStockPrice.Repository
{
    public class ReadExcelFile: IReadExcelFile
    {
        public Task<List<StockModel>> GetDataFromCVSFile(string filePath)
        {
            //totalLines = 0;
            var recordist = new List<StockModel>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line!.Split(',');

                        var stockData = new StockModel
                        {
                            StockID = values[0],
                            Timestamp = DateTime.ParseExact(values[1], "dd-MM-yyyy", CultureInfo.InvariantCulture),
                            StockPrice = double.Parse(values[2])
                        };
                        recordist.Add(stockData);
                }
            }
            return Task.FromResult(recordist);
        }
    }
}
