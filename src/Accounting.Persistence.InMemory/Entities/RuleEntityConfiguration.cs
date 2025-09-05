namespace Accounting.Persistence.InMemory.Entities;

using AccountingDashboard.Persistence.Abstractions.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RuleEntityConfiguration : IEntityTypeConfiguration<Rule>
{
    public void Configure(EntityTypeBuilder<Rule> entity)
    {
        entity.ToTable("Rule");

        entity.HasKey(x => x.Id);

        entity.Property(x => x.Client)
            .HasColumnType("nvarchar(100)");

        entity.Property(x => x.Program)
            .HasColumnType("nvarchar(250)");

        entity.Property(x => x.DepositDestination)
            .HasColumnType("nvarchar(50)");

        entity.Property(x => x.Updated)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(CURRENT_TIMESTAMP)");
    }
}
