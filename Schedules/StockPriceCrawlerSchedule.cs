namespace Mowei.Stock.Schedules
{
    /*
    public class StockPriceCrawlerSchedule : IInvocable
    {
        private StockRepository _stockRepository;

        public StockPriceCrawlerSchedule(StockRepository stockRepository)
        {
            this._stockRepository = stockRepository;
        }

        public async Task Invoke()
        {
            // DateTime date = _stockRepository.GetMaxDate().AddDays(1);
            DateTime date = DateTime.Now;
            while (DateTime.Compare(date.Date, DateTime.Now.Date) <= 0)
            {
                //假日不抓
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = date.AddDays(1);
                    continue;
                }
                else
                {
                    //await CrawlerStockByDate(date);
                    date = date.AddDays(1);
                    Thread.Sleep(7000);
                }
            }
        }

    }
        */
}
