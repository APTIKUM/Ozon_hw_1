namespace Domain.Reports;

public sealed record ClientsReport(
    IReadOnlyCollection<ClientSummary> ClientSummaries,
    DateTime CreatedDateTime = default);