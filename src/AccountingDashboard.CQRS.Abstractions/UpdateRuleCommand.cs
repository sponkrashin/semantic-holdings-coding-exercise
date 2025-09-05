namespace AccountingDashboard.CQRS.Abstractions;

using AccountingDashboard.CQRS.Abstractions.Models;

using MediatR;

public record UpdateRuleCommand : IRequest<RuleDTO>
{
    public int Id { get; set; }
    public string Client { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string DepositDestination { get; set; } = null!;
}
