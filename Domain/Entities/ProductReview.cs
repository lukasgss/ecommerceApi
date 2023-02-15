namespace ecommerceApi.Domain.Entities;

public class ProductReview
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public decimal Rating { get; set; }
    public DateOnly DateOfReview { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool RecommendsProduct { get; set; }
    public decimal GeneralQualityRating { get; set; }
    public decimal CostBenefitRating { get; set; }

    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
}