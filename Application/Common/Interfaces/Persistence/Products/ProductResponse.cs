using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Products;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    string Image,
    decimal Price,
    Guid CategoryId,
    ICollection<ProductReview>? ProductReviews);