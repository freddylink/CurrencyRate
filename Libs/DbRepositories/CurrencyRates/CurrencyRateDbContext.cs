using DbRepositories.CurrencyRates.Configurations;
using DbRepositories.CurrencyRates.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbRepositories
{
    public class CurrencyRateDbContext : DbContext
    {
        public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options)
            : base(options)
        { }

        //public DbSet<CurrencyRate> CurrencyRate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        }
    }
}
