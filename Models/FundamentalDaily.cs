using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mowei.Stock.Models
{
    [Table("FundamentalDaily")]
    public class FundamentalDaily
    {
        //"證券代號","證券名稱","殖利率(%)","股利年度","本益比","股價淨值比","財報年/季"
        [Key, Column(Order = 0)]
        public DateTime Date { get; set; }
        [Key, Column(Order = 1)]
        [DisplayName("證券代號")]
        public string Stock_id { get; set; }
        [DisplayName("殖利率(%)")]
        public decimal? Dividend_yield { get; set; }
        [DisplayName("本益比")]
        public decimal? Pe_ratio { get; set; }
        [DisplayName("股價淨值比")]
        public decimal? Price_book_ratio { get; set; }

    }
}