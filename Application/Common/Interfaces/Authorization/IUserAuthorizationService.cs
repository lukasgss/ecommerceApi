using System.Security.Claims;

namespace ecommerceApi.Application.Common.Interfaces.Authorization;

public interface IUserAuthorizationService
{
    string GetUserIdFromJwtToken(ClaimsPrincipal User);
}