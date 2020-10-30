using CsvHelper;
using CsvHelper.Configuration;
using Mowei.Stock.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mowei.Stock
{

    public class FundamentalDailyMap : ClassMap<FundamentalDaily>
    {


        public FundamentalDailyMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Date).Ignore();
            Map(m => m.Stock_id).ConvertUsing(row =>
            {
                var field = row.GetField("證券代號");
                if (!field.Contains("-"))
                    return field;
                else
                    return null;
            });
            Map(m => m.Dividend_yield).ConvertUsing(row =>
            {
                var field = row.GetField("殖利率(%)");
                if (!field.ToString().Contains("-"))
                    return Convert.ToDecimal(field);
                else
                    return null;
            });
            Map(m => m.Pe_ratio).ConvertUsing(row =>
            {
                var field = row.GetField("本益比");
                if (!field.ToString().Contains("-"))
                    return Convert.ToDecimal(field);
                else
                    return null;
            });
            Map(m => m.Price_book_ratio).ConvertUsing(row =>
            {
                var field = row.GetField("股價淨值比");
                if (!field.ToString().Contains("-"))
                    return Convert.ToDecimal(field);
                else
                    return null;
            });
        }
    }
}
