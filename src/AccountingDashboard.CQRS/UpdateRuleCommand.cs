namespace AccountingDashboard.CQRS;

using AccountingDashboard.Common;
using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.CQRS.Abstractions.Models;
using AccountingDashboard.Persistence.Abstractions;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UpdateRuleCommandHandler(IDatabaseContext databaseContext, ILogger<UpdateRuleCommandHandler> logger) : IRequestHandler<UpdateRuleCommand, RuleDTO>
{
    public async Task<RuleDTO> Handle(UpdateRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = await databaseContext.Rules.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (rule is null)
        {
            logger.LogError("Rule with id {Id} is not found", request.Id);
            throw new NotFoundException($"Rule with id {request.Id} is not found");
        }

        rule.Client = request.Client;
        rule.Program = request.Program;
        rule.DepositDestination = request.DepositDestination;
        rule.Updated = DateTime.UtcNow;

        await databaseContext.Commit(cancellationToken);

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
