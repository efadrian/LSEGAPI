namespace LSEGAPIStockPrice.Model
{
    public class StockModel
    {
        public string? StockID { get; set; }
        public DateTime Timestamp { get; set; }
        public double StockPrice { get; set; }
    }
}
