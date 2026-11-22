namespace Domain.Entities;

public sealed record Order(
    long Id,
    long ClientId,
    ICollection<OrderItem> OrderItems);