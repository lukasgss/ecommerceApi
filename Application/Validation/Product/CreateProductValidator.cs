using ecommerceApi.Application.Common.Interfaces.Persistence.Products;
using FluentValidation;

namespace ecommerceApi.Application.Validation.Product;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().Length(3, 255);
        RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(1000);
        RuleFor(x => x.Image).NotEmpty().NotNull().MaximumLength(1000);
        RuleFor(x => x.Price).NotEmpty().NotNull();
        RuleFor(x => x.CategoryId).NotEmpty().NotNull();
    }
}