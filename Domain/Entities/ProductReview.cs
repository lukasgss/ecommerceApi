namespace ecommerceApi.Domain.Entities;

public class ProductReview
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int Rating { get; set; }
    public DateOnly DateOfReview { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool RecommendsProduct { get; set; }
    public int GeneralQualityRating { get; set; }
    public int CostBenefitRating { get; set; }

    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}