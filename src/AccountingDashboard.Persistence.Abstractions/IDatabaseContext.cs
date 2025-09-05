namespace AccountingDashboard.Persistence.Abstractions;

using AccountingDashboard.Persistence.Abstractions.Entities;

using Microsoft.EntityFrameworkCore;

public interface IDatabaseContext
{
    public DbSet<Rule> Rules { get; set; }

    Task Commit(CancellationToken cancellationToken = default);
}
