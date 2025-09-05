namespace AccountingDashboard.Models;

using FluentValidation;

public record DeleteRuleRequest
{
    public int Id { get; set; }
}

internal class DeleteRuleRequestValidator : AbstractValidator<DeleteRuleRequest>
{
    public DeleteRuleRequestValidator()
    {
        this.RuleFor(x => x.Id).GreaterThan(0);
    }
}
