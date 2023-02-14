using ecommerceApi.Application.Services.Authentication;

namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(RegisterRequest request);
    Task<AuthenticationResult> LoginAsync(string email, string password);
}