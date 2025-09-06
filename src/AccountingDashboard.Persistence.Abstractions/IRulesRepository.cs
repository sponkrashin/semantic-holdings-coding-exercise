namespace AccountingDashboard.Persistence.Abstractions;

using AccountingDashboard.Persistence.Abstractions.Entities;

public interface IRulesRepository
{
    Task<IEnumerable<Rule>> GetRules(CancellationToken cancellationToken = default);

    Task<Rule> AddRule(Rule rule, CancellationToken cancellationToken = default);

    Task<Rule> UpdateRule(Rule rule, CancellationToken cancellationToken = default);

    Task DeleteRule(int ruleId, CancellationToken cancellationToken = default);
}
