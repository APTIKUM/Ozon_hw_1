namespace Domain.Reports;

public class ClientsReport(IReadOnlyCollection<ClientSummary> clientSummaries)
{
    public IReadOnlyCollection<ClientSummary> ClientSummaries { get; } = clientSummaries;

    public DateTime CreatedDateTime { get; } = DateTime.Now;
}