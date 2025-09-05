namespace AccountingDashboard.CQRS.Abstractions;

using AccountingDashboard.CQRS.Abstractions.Models;

using MediatR;

public record AddRuleCommand : IRequest<RuleDTO>
{
    public string Client { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string DepositDestination { get; set; } = null!;
}
