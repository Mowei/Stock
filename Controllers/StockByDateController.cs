using Microsoft.AspNetCore.Mvc;
using Mowei.Stock.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mowei.Stock.Controllers
{


    public class Datas
    {
        public List<List<string>> data9 { get; set; }
    }

    /// <summary>
    /// https://blog.shiangsoft.com/stock-price-clawer/
    /// 台股每日股價
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockByDateController : ControllerBase
    {
        //  private StockRepository _stockRepository;
        private ApplicationDbContext conn;

        public StockByDateController(ApplicationDbContext conn)
        {
            this.conn = conn;
            //  _stockRepository = new StockRepository((SqlConnection)conn);
        }

        [HttpGet("{date}")]
        public async Task<IEnumerable<TWStockPrice>> Get(string date)
        {
            using (var client = new HttpClient())
            {
                string json = await client.GetStringAsync($"https://www.twse.com.tw/exchangeReport/MI_INDEX?response=json&date={date}&type=ALLBUT0999&_=1586529875476");
                var resDatas = JsonSerializer.Deserialize<Datas>(json);
                var stockList = new List<TWStockPrice>();
                if (resDatas.data9 != null)
                {

                    foreach (var data in resDatas.data9)
                    {

                        stockList.Add(new TWStockPrice()
                        {
                            Date = DateTime.ParseExact(date, "yyyyMMdd", null),
                            Stock_id = data[0],
                            Trading_volume = decimal.Parse(data[2].Replace(",", "").Replace("--", "0")),
                            Trading_money = decimal.Parse(data[4].Replace(",", "").Replace("--", "0")),
                            Open = decimal.Parse(data[5].Replace(",", "").Replace("--", "0")),
                            Max = decimal.Parse(data[6].Replace(",", "").Replace("--", "0")),
                            Min = decimal.Parse(data[7].Replace(",", "").Replace("--", "0")),
                            Close = decimal.Parse(data[8].Replace(",", "").Replace("--", "0")),
                            Spread = decimal.Parse(data[10].Replace(",", "").Replace("--", "0")),
                            Trading_turnover = decimal.Parse(data[3].Replace(",", "").Replace("--", "0"))
                        });

                    }

                    conn.Stock.AddRange(stockList);
                    conn.SaveChanges();

                }
                return stockList;
            }
        }
    }
}
