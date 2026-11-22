using CsvHelper.Configuration.Attributes;

namespace Infrastructure.Models;

public sealed record OrderCsvRow
{
    [Name("client_id")]
    public required long ClientId { get; init; }

    [Name("order_id")]
    public required long OrderId { get; init; }

    [Name("item_name")]
    public required string ItemName { get; init; }

    [Name("item_quantity")]
    public required int ItemQuantity { get; init; }

    [Name("item_price")]
    public required decimal ItemPrice { get; init; }
}