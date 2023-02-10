namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);