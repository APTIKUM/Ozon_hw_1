namespace Domain.Reports;

public sealed record ClientSummary(
    long ClientId,
    string MostPurchasedItemName,
    decimal OrdersSum);