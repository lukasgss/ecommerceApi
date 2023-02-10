using ecommerceApi.Application.Common.Interfaces.Authentication;
using FluentValidation;

namespace ecommerceApi.Application.Validation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().NotNull().Length(2, 50);
        RuleFor(x => x.LastName).NotEmpty().NotNull().Length(2, 50);
        RuleFor(x => x.Password).NotEmpty().NotNull().Length(6, 255);
    }
}