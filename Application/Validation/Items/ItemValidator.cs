using ecommerceApi.Application.Common.Interfaces.Persistence.Items;
using FluentValidation;

namespace ecommerceApi.Application.Validation.Items;

public class ItemValidator : AbstractValidator<ItemRequest>
{
    public ItemValidator()
    {
        RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
        RuleFor(x => x.ProductId).NotEmpty().NotNull();
    }
}