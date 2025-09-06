namespace Accounting.Persistence.InMemory;

using AccountingDashboard.Common;
using AccountingDashboard.Persistence.Abstractions;
using AccountingDashboard.Persistence.Abstractions.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class RulesRepository(IDatabaseContext databaseContext, ILogger<RulesRepository> logger) : IRulesRepository
{
    public async Task<IEnumerable<Rule>> GetRules(CancellationToken cancellationToken)
    {
        var result = await databaseContext.Rules
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        return result;
    }

    public async Task<Rule> AddRule(Rule rule, CancellationToken cancellationToken = default)
    {
        databaseContext.Rules.Attach(rule);
        await databaseContext.Commit(cancellationToken);
        return rule;
    }

    public async Task<Rule> UpdateRule(Rule rule, CancellationToken cancellationToken = default)
    {
        var existingRule = await databaseContext.Rules.FirstOrDefaultAsync(x => x.Id == rule.Id, cancellationToken);
        if (existingRule is null)
        {
            logger.LogError("Rule with id {Id} is not found", rule.Id);
            throw new NotFoundException($"Rule with id {rule.Id} is not found");
        }

        existingRule.Client = rule.Client;
        existingRule.Program = rule.Program;
        existingRule.DepositDestination = rule.DepositDestination;
        existingRule.Updated = DateTime.UtcNow;

        await databaseContext.Commit(cancellationToken);

        return existingRule;
    }

    public async Task DeleteRule(int ruleId, CancellationToken cancellationToken = default)
    {
        var rule = await databaseContext.Rules.FirstOrDefaultAsync(x => x.Id == ruleId, cancellationToken);
        if (rule is null)
        {
            logger.LogError("Rule with id {Id} is not found", ruleId);
            throw new NotFoundException($"Rule with id {ruleId} is not found");
        }

        databaseContext.Rules.Remove(rule);
        await databaseContext.Commit(cancellationToken);

    }
}
