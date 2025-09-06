namespace AccountingDashboard.CQRS;

using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.CQRS.Abstractions.Models;
using AccountingDashboard.Persistence.Abstractions;

using MediatR;

public class GetRulesQueryHandler(IRulesRepository rulesRepository) : IRequestHandler<GetRulesQuery, IEnumerable<RuleDTO>>
{
    public async Task<IEnumerable<RuleDTO>> Handle(GetRulesQuery request, CancellationToken cancellationToken)
    {
        var rules = await rulesRepository.GetRules(cancellationToken);

        return rules
            .Select(x => new RuleDTO
            {
                Id = x.Id,
                Client = x.Client,
                Program = x.Program,
                DepositDestination = x.DepositDestination,
                UpdatedDate = x.Updated,
            })
            .ToArray();
    }
}
