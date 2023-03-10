namespace ecommerceApi.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public ICollection<ProductReview>? ProductReviews { get; set; }
}