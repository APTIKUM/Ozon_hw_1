using CsvHelper.Configuration.Attributes;

namespace Infrastructure.Models;

public class OrderCsvRow
{
    [Name("client_id")] public long ClientId { get; set; }

    [Name("order_id")] public long OrderId { get; set; }

    [Name("item_name")] public string ItemName { get; set; }

    [Name("item_quantity")] public int ItemQuantity { get; set; }

    [Name("item_price")] public decimal ItemPrice { get; set; }
}