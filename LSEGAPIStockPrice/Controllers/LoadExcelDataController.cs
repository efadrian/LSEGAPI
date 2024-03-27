using CsvHelper.Configuration;
using CsvHelper;
using LSEGAPIStockPrice.Model;
using Microsoft.AspNetCore.Mvc;
using LSEGAPIStockPrice.Repository;
using System;

namespace LSEGAPIStockPrice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadExcelDataController : ControllerBase
    {

        private readonly ILogger<LoadExcelDataController> _logger;

        public LoadExcelDataController(ILogger<LoadExcelDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDataPoints")]
        public async Task<IActionResult> GetDataPoints()
        {
            try
            {            
                int totalLines;
                List<StockModel> recordist = await ReadExcelFile.GetDataFromCVSFile($"Data/LSE/FLTR.csv");
                totalLines = recordist.Count;

                if (recordist.Count == 0)
                {
                    throw new Exception("The cvs file is empty.");
                }

                Random randomNumber = new Random();
                int index = randomNumber.Next(0, totalLines - 29);

                // get 30 de consecutive points
                List<StockModel> selectedDataPoints = recordist.GetRange(index, 30);
                return Ok(selectedDataPoints);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on processing file " + ex.Message);
            }
        }


        [HttpPost(Name = "ProcessDataPoints")]
        public async Task<IActionResult> ProcessDataPoints([FromBody] List<StockModel> stockDataList)
        {
            if (stockDataList == null || stockDataList.Count == 0)
            {
                return BadRequest("List of data points is empty!");
            }

            var list = await ProcessData.CalculateStandardDeviationAsync(stockDataList);
            return Ok(list);
        }
    }
}
