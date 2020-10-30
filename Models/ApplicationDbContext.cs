using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mowei.Stock.Models
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<TWStockPrice> Stock { get; set; }
        public virtual DbSet<FundamentalDaily> FundamentalDaily { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TWStockPrice>().HasKey(t => new { t.Date, t.Stock_id });
            modelBuilder.Entity<FundamentalDaily>().HasKey(t => new { t.Date, t.Stock_id });
            base.OnModelCreating(modelBuilder);
        }
    }
}
