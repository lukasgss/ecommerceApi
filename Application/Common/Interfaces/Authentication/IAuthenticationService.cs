using ecommerceApi.Application.Services.Authentication;

namespace ecommerceApi.Application.Common.Interfaces.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string firstName, string lastName, string email, string password);
    AuthenticationResult Login(string email, string password);
}