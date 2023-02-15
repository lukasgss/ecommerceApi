using ecommerceApi.Application.Common.Interfaces.Persistence.ProductReviews;
using FluentValidation;

namespace ecommerceApi.Application.Validation.ProductReview;

public class EditProductReviewValidator : AbstractValidator<EditProductReviewRequest>
{
    public EditProductReviewValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.Content).NotEmpty().NotNull().MaximumLength(255);
        RuleFor(x => x.Rating).NotEmpty().NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.RecommendsProduct).NotEmpty().NotNull();
        RuleFor(x => x.GeneralQualityRating).NotEmpty().NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.CostBenefitRating).NotEmpty().NotNull().InclusiveBetween(0, 5);
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
    }
}