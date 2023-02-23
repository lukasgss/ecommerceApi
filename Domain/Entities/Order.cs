namespace ecommerceApi.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; } = null!;
    public DateOnly? DateProductReceived { get; set; }

    public ICollection<Item> Items { get; set; } = null!;
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public ICollection<OrderStatus> OrderStatus { get; set; } = null!;
}