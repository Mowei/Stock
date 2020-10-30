using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mowei.Stock.Models
{

    [Table("TWStockPrice_History")]
    public class TWStockPrice
    {
        [Key, Column(Order = 0)]
        [DisplayName("交易日")]
        public DateTime Date { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(16)]
        [DisplayName("證券代號")]
        public string Stock_id { get; set; }

        [DisplayName("成交股數")]
        public decimal Trading_volume { get; set; }

        [DisplayName("成交金額")]
        public decimal Trading_money { get; set; }

        [DisplayName("開盤價")]
        public decimal Open { get; set; }

        [DisplayName("最高價")]
        public decimal Max { get; set; }

        [DisplayName("最低價")]
        public decimal Min { get; set; }

        [DisplayName("收盤價")]
        public decimal Close { get; set; }

        [DisplayName("漲跌價差")]
        public decimal Spread { get; set; }

        [DisplayName("成交筆數")]
        public decimal Trading_turnover { get; set; }

    }
}
