namespace Accounting.Persistence.InMemory;

using System.Reflection;

using AccountingDashboard.Persistence.Abstractions;
using AccountingDashboard.Persistence.Abstractions.Entities;

using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Rule> Rules { get; set; }

    public Task Commit(CancellationToken cancellationToken)
    {
        return this.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedRules(modelBuilder);
    }

    private static void SeedRules(ModelBuilder modelBuilder)
    {
        string[] clients = ["Client 1", "Client 2", "Monday", "Wrike", "Google", "Facebook", "Client 3", "Client 4"];
        string[] payments = ["Visa", "MC", "PayPal", "Amex"];
        var ticksFrom = DateTime.UtcNow.AddDays(-30).ToBinary();
        var ticksTo = DateTime.UtcNow.ToBinary();

        var rules = new List<Rule>();

        for (var i = 0; i < 20; ++i)
        {
            rules.Add(new Rule
            {
                Id = i + 1,
                Client = clients[Random.Shared.Next(0, clients.Length)],
                Program = $"Program {i + 1}",
                DepositDestination = $"{payments[Random.Shared.Next(0, payments.Length)]} {Random.Shared.Next(0, 9999):D4}",
                Updated = DateTime.FromBinary(Random.Shared.NextInt64(ticksFrom, ticksTo + 1)),
            });
        }

        modelBuilder.Entity<Rule>().HasData(rules);
    }
}
