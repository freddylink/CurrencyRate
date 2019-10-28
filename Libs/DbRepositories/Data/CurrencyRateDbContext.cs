using DbRepositories.CurrencyRates.Configurations;
using DbRepositories.Data.Configurations;
using DbRepositories.Data.Object;
using Microsoft.EntityFrameworkCore;

namespace DbRepositories.Data
{
    public class CurrencyRateDbContext : DbContext
    {
        public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options)
            : base(options)
        { }

        public DbSet<CurrencyRate> CurrencyRate { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyRateEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityConfiguration());
        }
    }
}
