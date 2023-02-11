using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using FluentValidation;

namespace ecommerceApi.Application.Validation;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}