namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public record LoginRequest(
    string Email,
    string Password);