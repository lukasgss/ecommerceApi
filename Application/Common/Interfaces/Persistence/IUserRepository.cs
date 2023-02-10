using ecommerceApi.Application.Common.Interfaces.Authentication;
using ecommerceApi.Application.Services.Authentication;
using ecommerceApi.Domain.Entities;

namespace ecommerceApi.Application.Common.Interfaces;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}