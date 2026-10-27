using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    public IReadOnlyCollection<Order> GetByClientIds(IReadOnlyCollection<long> clientIds);
}