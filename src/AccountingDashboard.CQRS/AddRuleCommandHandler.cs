namespace AccountingDashboard.CQRS;

using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.CQRS.Abstractions.Models;
using AccountingDashboard.Persistence.Abstractions;
using AccountingDashboard.Persistence.Abstractions.Entities;

using MediatR;

public class AddRuleCommandHandler(IRulesRepository rulesRepository) : IRequestHandler<AddRuleCommand, RuleDTO>
{
    public async Task<RuleDTO> Handle(AddRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = await rulesRepository.AddRule(new Rule
        {
            Client = request.Client,
            Program = request.Program,
            DepositDestination = request.DepositDestination,
        }, cancellationToken);

        return new RuleDTO
        {
            Id = rule.Id,
            Client = rule.Client,
            Program = rule.Program,
            DepositDestination = rule.DepositDestination,
            UpdatedDate = rule.Updated,
        };
    }
}
