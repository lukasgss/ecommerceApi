namespace ecommerceApi.Domain.Entities;

public class Item
{
    public Guid ItemId { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid OrderId { get; set; }
}