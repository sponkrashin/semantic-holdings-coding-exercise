namespace AccountingDashboard.CQRS.Abstractions;

using MediatR;

public record DeleteRuleCommand : IRequest
{
    public int Id { get; set; }
}
