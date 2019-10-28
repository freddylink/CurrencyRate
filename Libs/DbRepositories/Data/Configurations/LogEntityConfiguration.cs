using DbRepositories.Data.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbRepositories.Data.Configurations
{
    public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log)).HasKey(item => item.LogId);

            builder.Property(item => item.Message).HasMaxLength(255);
            builder.Property(item => item.Date);
        }
    }
}
