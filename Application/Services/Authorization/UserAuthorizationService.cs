using System.Security.Claims;
using ecommerceApi.Application.Common.Exceptions;
using ecommerceApi.Application.Common.Interfaces.Authorization;

namespace ecommerceApi.Application.Services.Authorization;

public class UserAuthorizationService : IUserAuthorizationService
{
    public string GetUserIdFromJwtToken(ClaimsPrincipal User)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            throw new ForbiddenException("User ID not allowed to create a review.");
        }
        return userId;
    }
}