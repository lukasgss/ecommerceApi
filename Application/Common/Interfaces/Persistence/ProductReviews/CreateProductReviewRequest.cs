namespace ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

public record CreateProductReviewRequest(
    string Title,
    string Content,
    decimal Rating,
    bool RecommendsProduct,
    decimal GeneralQualityRating,
    decimal CostBenefitRating,
    Guid ProductId);