namespace ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

public record ProductRelatedReviews(
    Guid Id,
    string Title,
    string Content,
    decimal Rating,
    DateOnly DateOfReview,
    bool RecommendsProduct,
    decimal GeneralQualityRating,
    decimal CostBenefitRating,
    Guid UserId);