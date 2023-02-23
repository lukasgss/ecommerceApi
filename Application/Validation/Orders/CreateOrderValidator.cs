using ecommerceApi.Application.Common.Interfaces.Persistence.Orders;
using ecommerceApi.Application.Validation.Items;
using FluentValidation;

namespace ecommerceApi.Application.Validation.Orders;

public class CreateOrderValidator : AbstractValidator<OrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.ShippingAddress).NotEmpty().NotNull();
        RuleForEach(x => x.Items).SetValidator(new ItemValidator());
    }
}