namespace Domain.Reports;

public class ClientSummary(long clientId, string mostPurchasedItemName, decimal ordersSum)
{
    public long ClientId { get; } = clientId;
    public string MostPurchasedItemName { get; } = mostPurchasedItemName;
    public decimal OrdersSum { get; } = ordersSum;
}