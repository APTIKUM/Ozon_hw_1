namespace Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}