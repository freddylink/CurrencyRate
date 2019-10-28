using DbRepositories.Data.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbRepositories.CurrencyRates.Configurations
{
    public class CurrencyRateEntityConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.ToTable(nameof(CurrencyRate)).HasKey(item => item.CurrencyRateId);

            builder.Property(item => item.BankId).HasMaxLength(255);
            builder.Property(item => item.CurrencyCode).IsRequired().HasMaxLength(3);
            builder.Property(item => item.Rate).HasColumnType("float");
            builder.Property(item => item.Timestamp).HasColumnType("Date");
        }
    }
}
