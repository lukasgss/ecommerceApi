using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Services.Authentication;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    void Add(User user);
}