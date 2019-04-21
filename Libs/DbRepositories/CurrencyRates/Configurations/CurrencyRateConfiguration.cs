using Microsoft.EntityFrameworkCore;
using DbRepositories.CurrencyRates.Object;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DbRepositories.CurrencyRates.Configurations
{
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            
            builder.ToTable(nameof(CurrencyRate)).HasKey(item => item.CurrencyRateId);
            builder.Property(item => item.BankId).HasMaxLength(255);
            builder.Property(item => item.CurrencyCode).IsRequired().HasMaxLength(255);
            builder.Property(item => item.Rate).HasMaxLength(255);
            builder.Property(item => item.Timestamp);
            
        }
    }
}
