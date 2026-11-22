using System.Globalization;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class CsvOrderRepository : IOrderRepository
{
    private const string Delimiter = ",";
    private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "orders_log.csv");

    public IReadOnlyCollection<Order> GetByClientIds(IReadOnlyCollection<long> clientIds)
    {
        var clientIdsSet = clientIds.ToHashSet();

        using var reader = new StreamReader(FilePath);

        using var csv = new CsvReader(reader,
            new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = Delimiter });

        var rows = csv.GetRecords<OrderCsvRow>()
            .Where(row => clientIdsSet.Contains(row.ClientId));

        var orders = rows
            .GroupBy(row => row.OrderId)
            .Select(group => new Order(
                group.Key,
                group.First().ClientId,
                group.Select(row => new OrderItem(
                    new Item(row.ItemName, row.ItemPrice),
                    row.ItemQuantity)
                ).ToList()
            )).ToList();

        return orders;
    }
}