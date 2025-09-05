namespace AccountingDashboard.CQRS;

using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.CQRS.Abstractions.Models;
using AccountingDashboard.Persistence.Abstractions;

using MediatR;

using Microsoft.EntityFrameworkCore;

public class GetRulesQueryHandler(IDatabaseContext databaseContext) : IRequestHandler<GetRulesQuery, IEnumerable<RuleDTO>>
{
    public async Task<IEnumerable<RuleDTO>> Handle(GetRulesQuery request, CancellationToken cancellationToken)
    {
        var result = await databaseContext.Rules
            .AsNoTracking()
            .Select(x => new RuleDTO
            {
                Id = x.Id,
                Client = x.Client,
                Program = x.Program,
                DepositDestination = x.DepositDestination,
                UpdatedDate = x.Updated,
            })
            .ToArrayAsync(cancellationToken);
        return result;
    }
}
