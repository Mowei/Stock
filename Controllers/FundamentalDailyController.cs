using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mowei.Stock.Models;

namespace Mowei.Stock.Controllers
{
    /// <summary>
    /// https://blog.shiangsoft.com/stock-fundamental-daily/
    /// 個股日本益比、殖利率及股價淨值比
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FundamentalDailyController : ControllerBase
    {

        private ApplicationDbContext conn;

        public FundamentalDailyController(ApplicationDbContext conn)
        {
            this.conn = conn;
        }

        [HttpGet("{date}")]
        public async Task<IEnumerable<FundamentalDaily>> Get(string date)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(
                    $"https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date={date}&selectType=ALL"
                );
                var bytes = await response.Content.ReadAsByteArrayAsync();
                var result = Encoding.GetEncoding(950).GetString(bytes);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new PlatformNotSupportedException($"目前無法爬取每日基本面資料...，{response.StatusCode}，{result}");

                var FundamentalDailyList = ReadCsv(result).ToList();
                for (var i=0;i< FundamentalDailyList.Count();i++)
                {
                    FundamentalDailyList[i].Date = DateTime.ParseExact(date, "yyyyMMdd", null);
                }
                conn.FundamentalDaily.AddRange(FundamentalDailyList);
                conn.SaveChanges();
                return ReadCsv(result);
            }
        }

        public IEnumerable<FundamentalDaily> ReadCsv(string data)
        {
            using (var reader = new StringReader(data))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.RegisterClassMap<FundamentalDailyMap>();
                while (csvReader.Read())
                {
                    FundamentalDaily fundamentalDaily = null;
                    try
                    {
                        if (!Regex.IsMatch(csvReader.Context.RawRecord, ".*,\r\n")) // 過濾不正常資料
                            continue;
                        fundamentalDaily = csvReader.GetRecord<FundamentalDaily>();
                    }
                    catch (CsvHelper.TypeConversion.TypeConverterException ex)
                    {
                        //_logger.LogDebug(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // _logger.LogWarning(ex.Message);
                    }
                    if (fundamentalDaily != null)
                        yield return fundamentalDaily;
                }
            }
        }

        /// <summary>
        /// 選股 & 簡易回測
        /// 本益比 < 15
        /// 股價淨值比< 2
        /// 殖利率> 4
        /// GetFundamentalDailyList(new DateTime(year, month, day), 15, 2, 4);
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pe_ratio"></param>
        /// <param name="price_book_ratio"></param>
        /// <param name="dividend_yield"></param>
        /// <returns></returns>
        public IEnumerable<FundamentalDaily> GetFundamentalDailyList(DateTime date, int pe_ratio, int price_book_ratio, int dividend_yield)
        {
            ///TODO
            /*
            月營收 > 前月營收
            月營收 > 前年同月營收
            EPS > 前季EPS
            股價 > 10
            股價 < 50
            */
            IEnumerable<FundamentalDaily> fundamentalDailyList = null;

            for (int i = 0; fundamentalDailyList == null || fundamentalDailyList.Count() == 0; i++)
            {
                fundamentalDailyList = conn.FundamentalDaily.Where(x => x.Date == date.AddDays(-i));
            }
            return fundamentalDailyList.Where(fundamentalDaily =>
                                    fundamentalDaily.Pe_ratio < pe_ratio &&
                                    fundamentalDaily.Price_book_ratio < price_book_ratio &&
                                    fundamentalDaily.Dividend_yield > dividend_yield);
        }
    }
}
