namespace AccountingDashboard.CQRS.Abstractions.Models;

public record RuleDTO
{
    public int Id { get; set; }
    public string Client { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string DepositDestination { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}
