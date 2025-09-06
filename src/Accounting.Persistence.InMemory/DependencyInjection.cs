namespace Accounting.Persistence.InMemory;

using AccountingDashboard.Persistence.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseSqlite("DataSource=file::memory:?cache=shared"));

        services
            .AddScoped<IDatabaseContext, DatabaseContext>(provider =>
            {
                var databaseContext = provider.GetRequiredService<DatabaseContext>();
                databaseContext.Database.EnsureCreated();
                return databaseContext;
            })
            .AddScoped<IRulesRepository, RulesRepository>();

        return services;
    }
}
