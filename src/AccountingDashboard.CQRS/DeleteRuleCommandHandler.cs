namespace AccountingDashboard.CQRS;

using AccountingDashboard.CQRS.Abstractions;
using AccountingDashboard.Persistence.Abstractions;

using MediatR;

public class DeleteRuleCommandHandler(IRulesRepository rulesRepository) : IRequestHandler<DeleteRuleCommand>
{
    public Task Handle(DeleteRuleCommand request, CancellationToken cancellationToken)
    {
        return rulesRepository.DeleteRule(request.Id, cancellationToken);
    }
}
