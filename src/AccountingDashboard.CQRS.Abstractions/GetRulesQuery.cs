namespace AccountingDashboard.CQRS.Abstractions;

using AccountingDashboard.CQRS.Abstractions.Models;

using MediatR;

public record GetRulesQuery : IRequest<IEnumerable<RuleDTO>>;
