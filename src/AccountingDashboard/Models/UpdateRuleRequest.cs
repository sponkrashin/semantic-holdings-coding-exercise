namespace AccountingDashboard.Models;

using FluentValidation;

public record UpdateRuleRequest
{
    public int Id { get; set; }
    public string? Client { get; set; }
    public string? Program { get; set; }
    public string? DepositDestination { get; set; }
}

internal class UpdateRuleRequestValidator : AbstractValidator<UpdateRuleRequest>
{
    public UpdateRuleRequestValidator()
    {
        this.RuleFor(x => x.Id).GreaterThan(0);
        this.RuleFor(x => x.Client).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.Program).NotEmpty().MaximumLength(250);
        this.RuleFor(x => x.DepositDestination).NotNull().MaximumLength(50);
    }
}
