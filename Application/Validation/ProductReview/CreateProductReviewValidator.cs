using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using FluentValidation;

namespace ecommerceApi.Application.Validation.ProductReview;

public class CreateProductReviewValidator : AbstractValidator<CreateProductReviewRequest>
{
    public CreateProductReviewValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(255);
        RuleFor(x => x.Content).NotNull().MaximumLength(255);
        RuleFor(x => x.Rating).NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.RecommendsProduct).NotNull();
        RuleFor(x => x.GeneralQualityRating).NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.CostBenefitRating).NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
    }
}