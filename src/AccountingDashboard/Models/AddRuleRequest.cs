namespace AccountingDashboard.Models;

using FluentValidation;

public record AddRuleRequest
{
    public string? Client { get; set; }
    public string? Program { get; set; }
    public string? DepositDestination { get; set; }
}

internal class AddRuleRequestValidator : AbstractValidator<AddRuleRequest>
{
    public AddRuleRequestValidator()
    {
        this.RuleFor(x => x.Client).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.Program).NotEmpty().MaximumLength(250);
        this.RuleFor(x => x.DepositDestination).NotNull().MaximumLength(50);
    }
}
