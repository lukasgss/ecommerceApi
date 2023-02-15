namespace ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;

public record ProductReviewResponse(
    Guid Id,
    string Title,
    string Content,
    decimal Rating,
    DateOnly DateOfReview,
    bool RecommendsProduct,
    decimal GeneralQualityRating,
    decimal CostBenefitRating,
    Guid UserId,
    Guid ProductId);