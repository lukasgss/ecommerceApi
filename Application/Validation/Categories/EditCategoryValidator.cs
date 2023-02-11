using ecommerceApi.Application.Common.Interfaces.Persistence.Categories;
using FluentValidation;

namespace ecommerceApi.Application.Validation.Categories;

public class EditCategoryValidator : AbstractValidator<EditCategoryRequest>
{
    public EditCategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}