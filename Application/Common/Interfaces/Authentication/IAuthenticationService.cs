using ecommerceApi.Application.Services.Authentication;

namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password);
    Task<AuthenticationResult> LoginAsync(string email, string password);
}