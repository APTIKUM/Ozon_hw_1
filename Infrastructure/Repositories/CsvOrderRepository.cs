using System.Globalization;
using Application.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class CsvOrderRepository : IOrderRepository
{
    private const string FilePath = "orders_log.csv";
    private const string Delimiter = ";";

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
            .Select(group => new Order
            {
                Id = group.Key,

                ClientId = group.First().ClientId,

                OrderItems = group.Select(row => new OrderItem
                {
                    Item = new Item
                    {
                        Name = row.ItemName,
                        Price = row.ItemPrice
                    },

                    Quantity = row.ItemQuantity
                }).ToList()
            }).ToList();

        return orders;
    }
}