namespace AccountingDashboard.CQRS;

using AccountingDashboard.Common;
using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.Persistence.Abstractions;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DeleteRuleCommandHandler(IDatabaseContext databaseContext, ILogger<DeleteRuleCommandHandler> logger) : IRequestHandler<DeleteRuleCommand>
{
    public async Task Handle(DeleteRuleCommand request, CancellationToken cancellationToken)
    {
        var rule = await databaseContext.Rules.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (rule is null)
        {
            logger.LogError("Rule with id {Id} is not found", request.Id);
            throw new NotFoundException($"Rule with id {request.Id} is not found");
        }

        databaseContext.Rules.Remove(rule);
        await databaseContext.Commit(cancellationToken);
    }
}
