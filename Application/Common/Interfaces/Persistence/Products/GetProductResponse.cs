using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

namespace ecommerceApi.Application.Common.Interfaces.Persistence.Productse;

public record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    string Image,
    decimal Price,
    Guid CategoryId,
    ICollection<ProductRelatedReviews> ProductReviews);