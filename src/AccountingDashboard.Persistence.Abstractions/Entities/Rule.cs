namespace AccountingDashboard.Persistence.Abstractions.Entities;

public class Rule
{
    public int Id { get; set; }
    public string Client { get; set; } = null!;
    public string Program { get; set; } = null!;
    public string DepositDestination { get; set; } = null!;
    public DateTime Updated { get; set; }
}
