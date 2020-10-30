using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mowei.Stock.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FundamentalDaily",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Stock_id = table.Column<string>(nullable: false),
                    Dividend_yield = table.Column<decimal>(nullable: true),
                    Pe_ratio = table.Column<decimal>(nullable: true),
                    Price_book_ratio = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundamentalDaily", x => new { x.Date, x.Stock_id });
                });

            migrationBuilder.CreateTable(
                name: "TWStockPrice_History",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Stock_id = table.Column<string>(maxLength: 16, nullable: false),
                    Trading_volume = table.Column<decimal>(nullable: false),
                    Trading_money = table.Column<decimal>(nullable: false),
                    Open = table.Column<decimal>(nullable: false),
                    Max = table.Column<decimal>(nullable: false),
                    Min = table.Column<decimal>(nullable: false),
                    Close = table.Column<decimal>(nullable: false),
                    Spread = table.Column<decimal>(nullable: false),
                    Trading_turnover = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWStockPrice_History", x => new { x.Date, x.Stock_id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundamentalDaily");

            migrationBuilder.DropTable(
                name: "TWStockPrice_History");
        }
    }
}
