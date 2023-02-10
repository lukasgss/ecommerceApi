namespace ecommerceApi.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Image { get; set; } = "https://i.redd.it/bemsen2f5bu71.png";
    public DateOnly RegisterDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public ICollection<ProductReview>? ProductReviews { get; set; }
}