namespace ecommerceApi.Domain.Entities;

public class OrderStatus
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Status { get; set; } = "Purchase received.";
    public DateTime Time { get; set; } = DateTime.UtcNow;

    public Order Order { get; set; } = null!;
    public Guid OrderId { get; set; }
}