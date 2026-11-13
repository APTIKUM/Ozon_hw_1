namespace Domain.Reports;

public class ClientsReport
{
    public required IReadOnlyCollection<ClientSummary> ClientSummaries { get; init; }

    public DateTime CreatedDateTime { get; init; } = DateTime.Now;
}