using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    IReadOnlyCollection<Order> GetByClientIds(IReadOnlyCollection<long> clientIds);
}