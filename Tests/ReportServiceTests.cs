using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Reports;
using FluentAssertions;

namespace Tests;

public sealed class ReportServiceTests
{
    [Test]
    public void GenerateClientsReport_CalculatesSumAndMostPurchasedItem()
    {
        var orders = new[]
        {
            CreateOrder(
                1,
                10,
                ("Item A", 100m, 2),
                ("Item B", 50m, 5))
        };
        var repository = new StubOrderRepository(orders);
        var writer = new StubReportWriter();
        var service = new ReportService(repository, writer);

        var report = service.GenerateClientsReport([10]);

        report.ClientSummaries.Should().ContainSingle();
        var summary = report.ClientSummaries.Single();
        summary.ClientId.Should().Be(10);
        summary.OrdersSum.Should().Be(450m);
        summary.MostPurchasedItemName.Should().Be("Item B");
    }

    [Test]
    public void GenerateClientsReport_DeduplicatesClientIds()
    {
        var repository = new StubOrderRepository(
            [
                CreateOrder(1, 10, ("Item A", 100m, 1))
            ]
        );

        var service = new ReportService(repository, new StubReportWriter());

        service.GenerateClientsReport([10, 10]);

        repository.RequestedClientIds.Should().Equal(10L);
    }

    [Test]
    public void GenerateClientsReport_ThrowsWhenClientHasNoOrders()
    {
        var service = new ReportService(
            new StubOrderRepository([]),
            new StubReportWriter());

        const int nonExistentClientId = 999;

        var action = () => service.GenerateClientsReport([nonExistentClientId]);

        action
            .Should()
            .Throw<ClientNotFoundException>()
            .Which.ClientId.Should().Be(nonExistentClientId);
    }

    private static Order CreateOrder(
        long orderId,
        long clientId,
        params (string Name, decimal Price, int Quantity)[] items)
    {
        var orderItems = items
            .Select(item => new OrderItem(
                new Item(item.Name, item.Price),
                item.Quantity))
            .ToList();

        return new Order(orderId, clientId, orderItems);
    }

    private sealed class StubOrderRepository(
        IReadOnlyCollection<Order> orders) : IOrderRepository
    {
        public IReadOnlyCollection<long> RequestedClientIds { get; private set; } = [];

        public IReadOnlyCollection<Order> GetByClientIds(
            IReadOnlyCollection<long> clientIds)
        {
            RequestedClientIds = clientIds;
            return orders;
        }
    }

    private sealed class StubReportWriter : IReportWriter
    {
        public ClientsReport? Report { get; private set; }

        public void Write(ClientsReport report)
        {
            Report = report;
        }
    }
}