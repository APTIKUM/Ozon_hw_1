using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Reports;

namespace Application.Services;

public class ReportService(
    IOrderRepository orderRepository,
    IReportWriter reportWriter)
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IReportWriter _reportWriter = reportWriter;

    public ClientsReport GenerateClientsReport(IReadOnlyCollection<long> clientIds)
    {
        var distinctClientIds = clientIds.Distinct()
            .ToList();

        var orders = _orderRepository.GetByClientIds(distinctClientIds);

        var summaries = distinctClientIds
            .Select(clientId => CreateClientSummary(clientId, orders))
            .ToList();

        return new ClientsReport(summaries);
    }

    public void WriteClientReport(ClientsReport clientsReport)
    {
        _reportWriter.Write(clientsReport);
    }

    private static ClientSummary CreateClientSummary(long clientId, IReadOnlyCollection<Order> orders)
    {
        var clientOrders = orders
            .Where(order => order.ClientId == clientId)
            .ToList();

        if (clientOrders.Count == 0) throw new ClientNotFoundException(clientId);

        var orderItems = clientOrders
            .SelectMany(order => order.OrderItems)
            .ToList();

        var ordersSum = orderItems
            .Sum(orderItem => orderItem.Item.Price * orderItem.Quantity);

        var mostPurchasedItemName = orderItems
            .GroupBy(orderItem => orderItem.Item.Name)
            .OrderByDescending(group =>
                group.Sum(orderItem => orderItem.Quantity))
            .First()
            .Key;

        return new ClientSummary(clientId, mostPurchasedItemName, ordersSum);
    }
}